using System;
using System.Collections.Generic;
using System.Linq;
using CSharpExtensions;
using CSharpExtensions.ContainerClasses;
using CSharpExtensions.ContainerClasses.Enums;
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

        #region SameNumberAs tests

        [TestMethod]
        public void SameNumberAsTest()
        {
            1.Upto(5).SameNumberAs(6.Upto(10)).ShouldBeTrue();
        }

        #endregion

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

        #region Many tests

        [TestMethod]
        public void ManyTestTrue()
        {
            1.Upto(2).Many().ShouldBeTrue();
        }

        [TestMethod]
        public void ManyTestFalse()
        {
            var ints = new List<int> { 1 };
            ints.Many().ShouldBeFalse();
        }

        [TestMethod]
        public void ManyTestPredicateTrue()
        {
            6.Upto(10).Many(n => n > 8).ShouldBeTrue();
        }

        [TestMethod]
        public void ManyTestPredicateFalse()
        {
            6.Upto(9).Many(n => n > 8).ShouldBeFalse();
        }

        [TestMethod]
        public void ManyTestPredicateNull()
        {
            var ints = new List<int>();
            ints.Many(null).ShouldBeTrue();
        }

        #endregion

        #region OneOf tests

        [TestMethod]
        public void OneOfTestTrue()
        {
            var ints = 1.WrapInList();
            ints.OneOf().ShouldBeTrue();
        }

        [TestMethod]
        public void OneOfTestFalse()
        {
            1.Upto(2).OneOf().ShouldBeFalse();
        }

        [TestMethod]
        public void OneOfTestPredicateTrue()
        {
            6.Upto(10).OneOf(n => n == 8).ShouldBeTrue();
        }

        [TestMethod]
        public void OneOfTestPredicateFalse()
        {
            var ints = new List<int> { 6, 7, 8, 9, 6 };
            ints.OneOf(n => n == 6).ShouldBeFalse();
        }

        [TestMethod]
        public void OneOfTestPredicateNull()
        {
            new List<int>().OneOf(null).ShouldBeTrue();
        }

        #endregion

        #region XOf tests

        [TestMethod]
        public void XOfTestTrue()
        {
            1.Upto(3).XOf(3).ShouldBeTrue();
        }

        [TestMethod]
        public void XOfTestFalse()
        {
            1.Upto(2).XOf(3).ShouldBeFalse();
        }

        [TestMethod]
        public void XOfTestPredicateTrue()
        {
            var ints = new List<int> { 6, 7, 8, 5, 3 };
            ints.XOf(n => n < 7, 3).ShouldBeTrue();
        }

        [TestMethod]
        public void XOfTestPredicateFalse1()
        {
            var ints = new List<int> { 6, 7, 8, 5, 3 };
            ints.XOf(n => n < 7, 2).ShouldBeFalse();
        }

        [TestMethod]
        public void XOfTestPredicateFalse2()
        {
            var ints = new List<int> { 6, 7, 8, 5, 3 };
            ints.XOf(n => n > 7, 3).ShouldBeFalse();
        }

        [TestMethod]
        public void XOfTestPredicateNull()
        {
            var ints = new List<int>();
            ints.XOf(null, 3).ShouldBeTrue();
        }

        #endregion

        #region frequencies tests

        [TestMethod]
        public void TestFrequenciesEmpty()
        {
            new List<int>().Frequencies().ShouldBeEmpty();
        }

        [TestMethod]
        public void TestFrequencies()
        {
            var ints = new List<int> {1, 1, 1, 2, 2, 3};
            var freqs = ints.Frequencies().ToList();
            freqs.ShouldNumber(3);
            freqs.ShouldContain(1.Pair(3));
            freqs.ShouldContain(2.Pair(2));
            freqs.ShouldContain(3.Pair(1));
        }

        #endregion

        #endregion

        #region conversion tests

        #region ToCollection tests

        [TestMethod]
        public void ToCollectionTestCount()
        {
            1.Upto(4).ToCollection().ShouldNumber(4);
        }

        [TestMethod]
        public void ToCollectionTest()
        {
            1.Upto(4).ToCollection().Sum().ShouldEqual(10);
        }

        #endregion

        #endregion

        #region adding and removing elements tests

        #region Append tests

        [TestMethod]
        public void AppendTestCount()
        {
            1.Upto(3).Append(4).ShouldNumber(4);
        }

        [TestMethod]
        public void AppendTestElement()
        {
            1.Upto(3).Append(4).ElementAt(3).ShouldEqual(4);
        }

        [TestMethod]
        public void AppendTestNull()
        {
            var nums = new List<string> { null, null };
            nums.Append(null);
        }

        #endregion

        #region Prepend tests

        [TestMethod]
        public void PrependTestCount()
        {
            1.Upto(3).Prepend(4).ShouldNumber(4);
        }

        [TestMethod]
        public void PrependTestElement()
        {
            1.Upto(3).Prepend(4).ElementAt(0).ShouldEqual(4);
        }

        [TestMethod]
        public void PrependTestNull()
        {
            var nums = new List<string> { null, null };
            nums.Prepend(null);
        }

        #endregion

        #region Exclude tests

        [TestMethod]
        public void ExcludeTest()
        {
            var nums = new List<int> { 1, 2, 3, 3, 2, 3, 4 };
            nums.Exclude(3).ShouldNumber(4);
        }

        [TestMethod]
        public void ExcludeTestEmpty()
        {
            var nums = new List<int>();
            nums.Exclude(3);
        }

        [TestMethod]
        public void ExcludeTestNull()
        {
            var nums = new List<string>();
            nums.Exclude(null);
        }

        #endregion

        #region RemoveLast tests

        [TestMethod]
        public void RemoveLastTestEmpty()
        {
            new List<string>().RemoveLast().ShouldBeEmpty();
        }

        [TestMethod]
        public void RemoveLastTestOne()
        {
            "a".WrapInList().RemoveLast().ShouldBeEmpty();
        }

        [TestMethod]
        public void RemoveLastTestThreeContainment()
        {
            new List<string> { "a", "b", "c" }.RemoveLast().ShouldContainExactly("a", "b");
        }

        #endregion

        #region RemoveFirst tests

        #region RemoveLast tests

        [TestMethod]
        public void RemoveFirstTestEmpty()
        {
            new List<string>().RemoveFirst().ShouldBeEmpty();
        }

        [TestMethod]
        public void RemoveFirstTestOne()
        {
            "a".WrapInList().RemoveFirst().ShouldBeEmpty();
        }

        [TestMethod]
        public void RemoveFirstTestThreeContainment()
        {
            new List<string> { "a", "b", "c" }.RemoveFirst().ShouldContainExactly("b", "c");
        }

        #endregion

        #endregion

        #region Rotate tests

        [TestMethod]
        public void TestRotate()
        {
            0.Upto(4).Rotate(EnumerableRotationDirection.Forwards).ShouldEqual(4, 0, 1, 2, 3);
            0.Upto(4).Rotate(EnumerableRotationDirection.Backwards).ShouldEqual(1, 2, 3, 4, 0);
        }

        #endregion

        #endregion
    }
}