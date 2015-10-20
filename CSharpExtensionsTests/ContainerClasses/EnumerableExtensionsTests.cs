using System;
using System.Collections.Generic;
using System.Linq;
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

        #region index handling tests

        #region FirstIndex tests

        [TestMethod]
        public void FirstIndexTest()
        {
            new List<string> { "a", "b", "c" }.FirstIndex(s => s == "b").ShouldEqual(1);
        }

        [TestMethod]
        public void FirstIndexTestNonexistent()
        {
            new List<string> { "a", "b", "c" }.FirstIndex(s => s == "d").ShouldEqual(-1);
        }

        [TestMethod]
        public void FirstIndexFirstTest()
        {
            new List<string> { "a", "b", "a" }.FirstIndex(s => s == "a").ShouldEqual(0);
        }

        [TestMethod]
        public void FirstIndexLastTest()
        {
            new List<string> { "a", "b", "c" }.FirstIndex(s => s == "c").ShouldEqual(2);
        }

        #endregion

        #region LastIndex tests

        [TestMethod]
        public void LastIndexTest()
        {
            new List<string> { "a", "b", "c" }.LastIndex(s => s == "b").ShouldEqual(1);
        }

        [TestMethod]
        public void LastIndexLastTest()
        {
            new List<string> { "c", "b", "c" }.LastIndex(s => s == "c").ShouldEqual(2);
        }

        [TestMethod]
        public void LastIndexFirstTest()
        {
            new List<string> { "a", "b", "c" }.LastIndex(s => s == "a").ShouldEqual(0);
        }

        [TestMethod]
        public void LastIndexTestNonexistent()
        {
            new List<string> { "a", "b", "c" }.LastIndex(s => s == "d").ShouldEqual(-1);
        }

        #endregion

        #region AllIndices tests

        [TestMethod]
        public void AllIndicesTestTrue()
        {
            1.Upto(10).AllIndices(n => n > -1).ShouldBeTrue();
        }

        [TestMethod]
        public void AllIndicesTestFalse()
        {
            1.Upto(10).AllIndices(n => n > 0).ShouldBeFalse();
        }

        #endregion

        #region EachIndex tests

        [TestMethod]
        public void EachIndexTest()
        {
            var total = 0;
            Action<int> addToTotal = n => total += n;
            1.Upto(5).EachIndex(addToTotal);
            total.ShouldEqual(10);
        }

        #endregion

        #region CountIndices tests

        [TestMethod]
        public void TestCountIndices()
        {
            var ints = 1.Upto(10);
            var enumerable = ints as int[] ?? ints.ToArray();
            enumerable.CountIndices(i => enumerable.ElementAt(i) < 6).ShouldEqual(5);
        }

        #endregion

        #endregion

        #region counting tests

        #region None tests

        [TestMethod]
        public void NoneTestTrue()
        {
            new List<int>().None().ShouldBeTrue();
        }

        [TestMethod]
        public void NoneTestFalse()
        {
            6.Upto(8).None().ShouldBeFalse();
        }

        [TestMethod]
        public void NoneTestPredicateTrue()
        {
            6.Upto(8).None(n => n > 8).ShouldBeTrue();
        }

        [TestMethod]
        public void NoneTestPredicateFalse()
        {
            6.Upto(8).None(n => n > 6).ShouldBeFalse();
        }

        [TestMethod]
        public void NoneTestPredicateNull()
        {
            6.Upto(8).None(null).ShouldBeTrue();
        }

        #endregion

        #endregion
    }
}