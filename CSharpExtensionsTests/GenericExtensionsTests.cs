using System;
using System.Xml;
using CSharpExtensions;
using CSharpExtensions.Text;
using CSharpExtensionsTests.GeneralFixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests
{
    [TestClass]
    public class GenericExtensionsTests
    {
        #region containment

        #region in tests

        [TestMethod]
        public void InTestTrue()
        {
            "a".In("a", "b", "c").ShouldBeTrue();
        }

        [TestMethod]
        public void InTestFalse()
        {
            "a".In("z", "b", "c").ShouldBeFalse();
        }

        [TestMethod]
        public void InTestNull()
        {
            "a".In(null, "b", "c").ShouldBeFalse();
        }

        #endregion

        #region notin tests

        [TestMethod]
        public void NotInTestTrue()
        {
            "a".NotIn("b", "c", "d").ShouldBeTrue();
        }

        [TestMethod]
        public void NotInTestFalse()
        {
            "a".NotIn("a", "b").ShouldBeFalse();
        }

        #endregion

        #region clamp tests

        [TestMethod]
        public void ClampTestHigher()
        {
            50.Clamp(0, 20).ShouldEqual(20);
        }

        [TestMethod]
        public void ClampTestLower()
        {
            (-50).Clamp(0, 20).ShouldEqual(0);
        }

        [TestMethod]
        public void ClampTestReversed()
        {
            50.Clamp(20, 0).ShouldEqual(20);
        }

        #endregion

        #region between tests

        [TestMethod]
        public void BetweenTestTrue()
        {
            var between = new DateTime(2021, 1, 1);
            var from = new DateTime(1941, 3, 1);
            var to = new DateTime(2031, 12, 20);

            between.Between(from, to).ShouldBeTrue();
        }

        [TestMethod]
        public void BetweenTestFalse1()
        {
            var between = new DateTime(2121, 1, 1);
            var from = new DateTime(1941, 3, 1);
            var to = new DateTime(2031, 12, 20);

            between.Between(from, to).ShouldBeFalse();
        }

        [TestMethod]
        public void BetweenTestFalse2()
        {
            var between = new DateTime(1821, 1, 1);
            var from = new DateTime(1941, 3, 1);
            var to = new DateTime(2031, 12, 20);

            between.Between(from, to).ShouldBeFalse();
        }

        #endregion

        #endregion

        #region chaining

        [TestMethod]
        public void ReturnActionTest()
        {
            var total = 0;

            1.Return(a => total += a).ShouldEqual(1);
        }

        #endregion

        #region tuple handling

        #region pair tests

        [TestMethod]
        public void PairTest1()
        {
            "1".Pair(2).Item1.ShouldEqual("1");
        }

        [TestMethod]
        public void PairTest2()
        {
            "1".Pair(2).Item2.ShouldEqual(2);
        }

        [TestMethod]
        public void PairTestNull()
        {
            "1".Pair<string, string>(null);
        }

        #endregion

        #region triple tests

        [TestMethod]
        public void TripleTest1()
        {
            "1".Triple(2, "3").Item1.ShouldEqual("1");
        }

        [TestMethod]
        public void TripleTest2()
        {
            "1".Triple(2, "3").Item2.ShouldEqual(2);
        }

        [TestMethod]
        public void TripleTest3()
        {
            "1".Triple(2, "3").Item3.ShouldEqual("3");
        }

        [TestMethod]
        public void TripleTestNull()
        {
            "1".Triple<string, string, string>(null, null);
        }

        #endregion

        #endregion

        #region conversion tests

        [TestMethod]
        public void WrapInListTest()
        {
            var list = 1.WrapInList();
            list.ShouldNumber(1);
            list.ShouldContain(1);
        }

        [TestMethod]
        public void ToXmlTest()
        {
            var bob = new Person { Name = "Bob", Age = 43 };
            var xml = bob.ToXml();
            var flattened = xml.FlattenXml();
            flattened.ShouldEqual(
                new Tuple<XmlNodeType,string>(XmlNodeType.XmlDeclaration, "xml"),
                new Tuple<XmlNodeType, string>(XmlNodeType.Element, "Person"),
                new Tuple<XmlNodeType, string>(XmlNodeType.Element, "Name"),
                new Tuple<XmlNodeType, string>(XmlNodeType.Text, "Bob"),
                new Tuple<XmlNodeType, string>(XmlNodeType.EndElement, "Name"),
                new Tuple<XmlNodeType, string>(XmlNodeType.Element, "Age"),
                new Tuple<XmlNodeType, string>(XmlNodeType.Text, "43"),
                new Tuple<XmlNodeType, string>(XmlNodeType.EndElement, "Age"),
                new Tuple<XmlNodeType, string>(XmlNodeType.EndElement, "Person")
                );
        }

        #endregion

        #region null handling tests

        [TestMethod]
        public void TestDefaultIfNull()
        {
            Func<string, int> f = s => 2;
            string str = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            str.DefaultIfNull(f).ShouldEqual(0);
        }

        [TestMethod]
        public void TestDefaultIfNull2()
        {
            Func<string, string> f = s => "a";
            string str = null;
            Assert.IsNull(str.DefaultIfNull(f));
        }

        [TestMethod]
        public void TestDefaultIfNull3()
        {
            Func<string, int> f = s => 2;
            "".DefaultIfNull(f).ShouldEqual(2);
        }

        [TestMethod]
        public void TestToStringOrEmptyNotNull()
        {
            "a".ToStringOrEmpty().ShouldEqual("a");
        }

        [TestMethod]
        public void TestToStringOrEmptyNull()
        {
            string str = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            str.ToStringOrEmpty().ShouldEqual("");
        }

        #endregion
    }
}