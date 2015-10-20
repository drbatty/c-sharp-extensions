using CSharpExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests
{
    [TestClass]
    public class GenericExtensionsTests
    {
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

        [TestMethod]
        public void WrapInListTest()
        {
            var list = 1.WrapInList();
            list.ShouldNumber(1);
            list.ShouldContain(1);
        }
    }
}