using System.Collections.Generic;
using CSharpExtensions;
using CSharpExtensions.ContainerClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests.ContainerClasses
{
    [TestClass]
    public class EnumerableExtensionsTests
    {
        #region comprehension tests

        #region Each tests

        [TestMethod]
        public void EachTest()
        {
            var total = 0;
            1.Upto(5).Each(num => total += num);

            total.ShouldEqual(15);
        }

        [TestMethod]
        public void EachTestEmpty()
        {
            var nums = new List<int>();

            var total = 0;
            nums.Each(num => total += num);

            total.ShouldEqual(0);
        }

        [TestMethod]
        public void EachTestNull()
        {
            var nums = new List<int>();
            nums.Each(null);
        }

        #endregion

        #region EachPair tests

        [TestMethod]
        public void TestEachPair()
        {
            var total = 0;
            1.Upto(4).EachPair((x,y) => total += 3*x + 5*y);
            total.ShouldEqual(320);
        }

        #endregion

        #region Inject tests

        [TestMethod]
        public void InjectTest()
        {
            1.Upto(5).Inject(5, (acc, t) => acc + t).ShouldEqual(20);
        }

        [TestMethod]
        public void InjectTestNull()
        {
            1.Upto(5).Inject(5, null);
        }

        #endregion

        #region product tests

        [TestMethod]
        public void ProductOfIntegersShouldEqualFactorial()
        {
            1.Upto(5).Product().ShouldEqual(120);
        }

        #endregion

        #endregion
    }
}