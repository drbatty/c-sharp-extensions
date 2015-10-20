using CSharpExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests
{
    [TestClass]
    public class IntExtensionsTests
    {
        [TestMethod]
        public void UpToTestCount()
        {
            1.Upto(5).ShouldNumber(5);
        }

        [TestMethod]
        public void UpToTestCount2()
        {
            6.Upto(11).ShouldNumber(6);
        }

        [TestMethod]
        public void DoUptoTest()
        {
            var total = 0;
            1.DoUpto(5, i => total++);
            total.ShouldEqual(5);
        }

        [TestMethod]
        public void DoUptoTestNull()
        {
            1.DoUpto(5, null);
        }

        [TestMethod]
        public void DoZeroUptoTest()
        {
            var total = 0;
            5.DoZeroUpto(i => total++);
            total.ShouldEqual(6);
        }

        [TestMethod]
        public void DoTest()
        {
            var total = 0;
            5.Do(i => total++);
            total.ShouldEqual(5);
        }
    }
}