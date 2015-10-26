using System.Collections.Generic;
using CSharpExtensions.ContainerClasses;
using CSharpExtensions.Mathematical.Sets;
using CSharpExtensionsTests.GeneralFixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests.ContainerClasses
{
    [TestClass]
    public class DictionaryExtensionsTests
    {
        #region fixtures

        private static readonly Dictionary<string, int> Dict = new Dictionary<string, int>
        {
            {"a", 1},
            {"b", 7},
            {"c", 9}
        };

        public static Dictionary<string, string> SimpleDict = new Dictionary<string, string>
        {
            {"a", "x"},
            {"b", "y"},
            {"c", "z"}
        };

        #endregion

        #region iteration

        [TestMethod]
        public void AllKeysTestTrue()
        {
            Dict.AllKeys(key => key == "a" || key == "b" || key == "c").ShouldBeTrue();
        }

        [TestMethod]
        public void AllKeysTestFalse()
        {
            Dict.AllKeys(key => key == "a" || key == "b").ShouldBeFalse();
        }

        [TestMethod]
        public void EachKeyTest()
        {
            var keys = "";
            Dict.EachKey(key => keys += key);
            keys.ShouldEqual("abc");
        }

        [TestMethod]
        public void EachValueTest()
        {
            var total = 0;
            Dict.EachValue(value => total += value);
            total.ShouldEqual(17);
        }

        [TestMethod]
        public void AllValuesTest()
        {
            Dict.AllValues(v => v < 10 && v >= 1).ShouldBeTrue();
        }

        [TestMethod]
        public void EachKeyValueTest()
        {
            var s = string.Empty;
            Dict.EachKeyValue((k, v) => s += k + v);
            s.ShouldEqual("a1b7c9");
        }

        [TestMethod]
        public void AllKeyValuesTest()
        {
            Dict.AllKeyValues((k, v) => (k + v).Length == 2).ShouldBeTrue();
        }

        [TestMethod]
        public void InjectKeyValuesTest()
        {
            Dict.InjectKeyValues(string.Empty, (acc, s, i) => acc + s + i).ShouldEqual("a1b7c9");
        }

        [TestMethod]
        public void InjectKeyValuesNullTest()
        {
            Dict.InjectKeyValues("a", null).ShouldEqual("a");
        }

        [TestMethod]
        public void SelectKeyValuesTest()
        {
            Dict.SelectKeyValues((key, value) => key + value).ShouldContainExactly("a1", "b7", "c9");
        }

        #endregion

        #region insertion

        [TestMethod]
        public void SetTest()
        {
            var initialDict = new Dictionary<string, int>
                {
                    {"a", 0},
                    {"b", 0},
                    {"c", 0}
                };
            var newDict = new Dictionary<string, int>
                {
                    {"a", 2},
                    {"b", 3},
                    {"c", 4}
                };
            initialDict.Set(newDict);
            initialDict["a"].ShouldEqual(2);
            initialDict["b"].ShouldEqual(3);
            initialDict["c"].ShouldEqual(4);
        }

        [TestMethod]
        public void SetIfNotNullTestFirstNull()
        {
            var dict = new Dictionary<string, int>();
            dict.SetIfNotNull(null, 1);

            dict.Keys.ShouldNumber(0);
        }

        [TestMethod]
        public void SetIfNotNullTestSecondNull()
        {
            var dict = new Dictionary<string, string>();
            dict.SetIfNotNull("a", null);

            dict.Keys.ShouldNumber(0);
        }

        [TestMethod]
        public void SetIfNotNullTest()
        {
            var dict = new Dictionary<string, string>();
            dict.SetIfNotNull("a", "b");

            dict.Keys.ShouldNumber(1);
        }

        #endregion

        #region conversion

        [TestMethod]
        public void ToStringsTest()
        {
            var toStrings = Dict.ToStrings();
            toStrings["a"].ShouldEqual("1");
            toStrings["b"].ShouldEqual("7");
            toStrings["c"].ShouldEqual("9");
        }

        #endregion

        #region containment

        [TestMethod]
        public void ContainsKeysTestTrue()
        {
            SimpleDict.ContainsKeys("b", "c").ShouldBeTrue();
        }

        [TestMethod]
        public void ContainsKeysTestFalse()
        {
            SimpleDict.ContainsKeys("b", "c", "z").ShouldBeFalse();
        }

        [TestMethod]
        public void ContainsKeysTestNull()
        {
            SimpleDict.ContainsKeys(null, null, null).ShouldBeTrue();
        }

        [TestMethod]
        public void ContainsValuesTestTrue()
        {
            SimpleDict.ContainsValues("x", "z").ShouldBeTrue();
        }

        [TestMethod]
        public void ContainsValuesTestFalse()
        {
            SimpleDict.ContainsValues("b", "x", "z").ShouldBeFalse();
        }

        [TestMethod]
        public void ContainsValuesTestNull()
        {
            SimpleDict.ContainsValues(null, null, null).ShouldBeTrue();
        }

        [TestMethod]
        public void GetOrDefaultTestNull()
        {
            Assert.IsNull(new Dictionary<string, string>().GetOrDefault("a"));
        }

        #endregion

        #region lookup

        [TestMethod]
        public void GetOrDefaultTestNonNull()
        {
            new Dictionary<string, string> { { "a", "b" } }.GetOrDefault("a").ShouldEqual("b");
        }

        [TestMethod]
        public void GetOrInsertNewTestDoesntExist()
        {
            var dict = new Dictionary<string, Person>();
            dict.Upsert("michael");
            dict.ContainsKey("michael").ShouldBeTrue();
            Assert.IsNull(dict["michael"].Name);
            dict["michael"].Age.ShouldEqual(0);
        }

        [TestMethod]
        public void GetOrInsertNewTestExists()
        {
            var dict = new Dictionary<string, Person> { { "michael", new Person { Name = "Michael", Age = 43 } } };
            dict.Upsert("michael");
            dict["michael"].Name.ShouldEqual("Michael");
            dict["michael"].Age.ShouldEqual(43); // fail. it's 44 now
        }

        #endregion

        #region counting

        [TestMethod]
        public void CountKeysTest()
        {
            var dict = new Dictionary<string, int>
                {
                    {"a", 2},
                    {"b", 3},
                    {"cd", 4}
                };
            dict.CountKeys(k => k.Length == 1).ShouldEqual(2);
        }

        #endregion

        #region restriction, image and preimage

        [TestMethod]
        public void TestRestrict()
        {
            Dict.Restrict(Dict.Keys.ToSet()).ShouldBeEquivalentMappingTo(Dict);
            Dict.Restrict(new Set<string> { "a" }).ShouldBeEquivalentMappingTo(new Dictionary<string, int> { { "a", 1 } });
            Dict.Restrict(new Set<string>()).ShouldBeEquivalentMappingTo(new Dictionary<string, int>());
        }

        [TestMethod]
        public void TestImage()
        {
            Dict.Image(Dict.Keys.ToSet()).ShouldEqual(Dict.Values.ToSet());
            Dict.Image(new Set<string> { "a" }).ShouldEqual(new Set<int> { 1 });
            Dict.Image(new Set<string>()).ShouldEqual(new Set<int>());
        }

        [TestMethod]
        public void TestPreimage()
        {
            var dict = new Dictionary<string, int>
                {
                    {"a", 2},
                    {"b", 3},
                    {"c", 2}
                };
            dict.Preimage(dict.Values.ToSet()).ShouldEqual(dict.Keys.ToSet());
            dict.Preimage(new Set<int> { 2 }).ShouldEqual(new Set<string> { "a", "c" });
            dict.Preimage(new Set<int>()).ShouldEqual(new Set<string>());
        }

        [TestMethod]
        public void TestRestrictValues()
        {
            Dict.Restrict(Dict.Values.ToSet()).ShouldBeEquivalentMappingTo(Dict);
            Dict.Restrict(new Set<int>()).ShouldBeEquivalentMappingTo(new Dictionary<string, int>());
            Dict.Restrict(new Set<int> { 7 }).ShouldBeEquivalentMappingTo(new Dictionary<string, int> { { "b", 7 } });
        }

        #endregion

        #region filtering

        [TestMethod]
        public void WhereKeysTest()
        {
            Dict.WhereKeys(key => key == "a" || key == "c")
                .ShouldBeEquivalentMappingTo(new Dictionary<string, int>
                    {
                        {"a", 1}, {"c", 9}
                    });
        }

        [TestMethod]
        public void WhereValuesTest()
        {
            Dict.WhereValues(value => value > 1)
                .ShouldBeEquivalentMappingTo(new Dictionary<string, int>
                    {
                        {"b", 7}, {"c", 9}
                    });
        }

        #endregion

        #region formatting

        [TestMethod]
        public void InspectTest()
        {
            Dict.Inspect().ShouldEqual("{a->1, b->7, c->9}");
        }

        [TestMethod]
        public void InspectTestEmpty()
        {
            new Dictionary<string, int>().Inspect().ShouldEqual("{}");
        }

        #endregion
    }
}
