using System.Collections.Generic;
using CSharpExtensions.ContainerClasses;
using CSharpExtensions.Mathematical.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests.ContainerClasses
{
    [TestClass]
    public class DictionaryExtensionsTests
    {
        #region fixture

        private static readonly Dictionary<string, int> Dict = new Dictionary<string, int>
        {
            {"a", 1},
            {"b", 7},
            {"c", 9}
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
    }
}
