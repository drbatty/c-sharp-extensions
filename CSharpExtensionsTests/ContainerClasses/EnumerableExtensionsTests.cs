using System;
using System.Collections.Generic;
using System.Linq;
using CSharpExtensions;
using CSharpExtensions.ContainerClasses;
using CSharpExtensions.ContainerClasses.Enums;
using CSharpExtensions.Text;
using CSharpExtensionsTests.GeneralFixtures;
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
            // ReSharper disable once IteratorMethodResultIsIgnored
            new List<string> { null, null }.Append(null);
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
            // ReSharper disable once IteratorMethodResultIsIgnored
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

        #region filtering tests

        #region NotDefault tests

        [TestMethod]
        public void NotDefaultTest()
        {
            var strings = new List<string> { string.Empty, null, null, string.Empty };
            strings.NotDefault().ShouldNumber(2);
        }

        [TestMethod]
        public void NotDefaultTestEmpty()
        {
            var strings = new List<string>();
            strings.NotDefault();
        }

        #endregion

        #region WhereIf tests

        [TestMethod]
        public void WhereIfTestFalse()
        {
            1.Upto(5).WhereIf(false, n => n < 3).ShouldNumber(5);
        }

        [TestMethod]
        public void WhereIfTestTrue()
        {
            1.Upto(5).WhereIf(true, n => n < 3).ShouldNumber(2);
        }

        [TestMethod]
        public void WhereIfTestNull()
        {
            1.Upto(5).WhereIf(true, null);
        }

        #endregion

        #region WhereSelect tests

        [TestMethod]
        public void WhereSelectTest()
        {
            1.Upto(5).WhereSelect(n => n >= 3, n => n * n).Sum().ShouldEqual(50);
        }

        [TestMethod]
        public void WhereSelectTestNull()
        {
            1.Upto(5).WhereSelect<int, int>(null, null);
        }

        #endregion

        #region Distinct tests

        [TestMethod]
        public void DistinctTest()
        {
            var oldBob = new Person { Name = "Bob", Age = 70 };
            var youngBob = new Person { Name = "Bob", Age = 17 };
            var rob = new Person { Name = "Rob", Age = 30 };
            var people = new List<Person> { oldBob, youngBob, rob };
            var peopleWithDifferentNames = people.Distinct(person => person.Name);

            peopleWithDifferentNames.ShouldNumber(2);
        }

        #endregion

        #region WhereNot tests

        [TestMethod]
        public void WhereNotTest()
        {
            (1.Upto(3)).WhereNot(n => n > 2).ShouldNumber(2);
        }

        #endregion

        #region search tests

        public static List<string> SearchList
        {
            get { return new List<string> { "ab", "bCD", "cDe" }; }
        }

        [TestMethod]
        public void Search_with_empty_search_term_should_return_everything()
        {
            SearchList.Search(s => s, "").ShouldNumber(3);
        }

        [TestMethod]
        public void Search_for_complete_string_should_return_correct_result()
        {
            SearchList.Search(s => s, "bCD").ShouldContainExactly("bCD");
        }

        [TestMethod]
        public void Search_should_be_case_insensitive()
        {
            SearchList.Search(s => s, "bcd").ShouldContainExactly("bCD");
        }

        [TestMethod]
        public void Search_should_find_substrings()
        {
            SearchList.Search(s => s, "cd").ShouldContainExactly("bCD", "cDe");
        }

        #endregion

        #endregion

        #region randomization tests

        #region shuffle tests

        [TestMethod]
        public void ShuffleTestEmpty()
        {
            var ints = new List<int>();
            ints.Shuffle();
        }

        [TestMethod]
        public void ShuffleTestNull()
        {
            var nulls = new List<string> { null, null, null };
            nulls.Shuffle();
        }

        [TestMethod]
        public void ShuffleTestCount()
        {
            1.Upto(3).Shuffle().ShouldNumber(3);
        }


        #endregion

        #region TakeRandom tests

        [TestMethod]
        public void TakeRandomNumberTest()
        {
            (1.Upto(10)).TakeRandom(3).ShouldNumber(3);
        }

        [TestMethod]
        public void TakeRandomNumberContainTest()
        {
            (1.Upto(10)).ShouldContain(1.Upto(10).TakeRandom(3));
        }

        [TestMethod]
        public void TakeRandomTest()
        {
            (1.Upto(10)).ShouldContain(1.Upto(10).TakeRandom());
        }

        #endregion

        #endregion

        #region mathematical tests

        #region Maximizer tests

        [TestMethod]
        public void MaximizerTest()
        {
            Func<int, double> doubleVal = n => (double)n;
            1.Upto(10).Maximizer(doubleVal).ShouldEqual(new Tuple<int, double>(10, 10));
        }

        [TestMethod]
        public void MaximizerTestNull()
        {
            Assert.IsNull(1.Upto(10).Maximizer(null));
        }

        [TestMethod]
        public void MaximizerTestEmpty()
        {
            Func<int, double> doubleVal = n => n;
            Assert.IsNull(new List<int>().Maximizer(doubleVal));
        }

        #endregion

        #region MinPair tests

        [TestMethod]
        public void MinPairTest()
        {
            Func<int, int, double> sum = (m, n) => m * n;
            1.Upto(10).Min(1.Upto(10), sum).ShouldEqual(1);
        }

        [TestMethod]
        public void MinPairTestNull()
        {
            1.Upto(10).Min(1.Upto(10), null).ShouldEqual(default(double));
        }

        [TestMethod]
        public void MinPairTestEmpty1()
        {
            Func<int, int, double> sum = (m, n) => m * n;
            new List<int>().Min(1.Upto(10), sum).ShouldEqual(default(double));
        }

        [TestMethod]
        public void MinPairTestEmpty2()
        {
            Func<int, int, double> sum = (m, n) => m * n;
            1.Upto(10).Min(new List<int>(), sum).ShouldEqual(default(double));
        }

        #endregion

        #region Minimizers tests

        [TestMethod]
        public void MinimizersTest()
        {
            1.Upto(10).Minimizers(1.Upto(10), (d1, d2) => d1 * d2).Item1.ShouldEqual(new Tuple<int, int>(1, 1));
        }

        [TestMethod]
        public void MinimizersTest2()
        {
            1.Upto(10).Minimizers(1.Upto(10), (d1, d2) => d1 * d2).Item2.ShouldEqual(1.0);
        }

        [TestMethod]
        public void MinimizersTestEmpty1()
        {
            Assert.IsNull(new List<int>().Minimizers(1.Upto(10), (d1, d2) => d1 * d2));
        }

        [TestMethod]
        public void MinimizersTestEmpty2()
        {
            Assert.IsNull(1.Upto(10).Minimizers(new List<int>(), (d1, d2) => d1 * d2));
        }

        [TestMethod]
        public void MinimizersTestLambdaNull()
        {
            Assert.IsNull(1.Upto(10).Minimizers(1.Upto(10), null));
        }

        #endregion

        #region Combinations tests

        [TestMethod]
        public void CombinationsTestTrue()
        {
            0.ArrayUpto(1).Combinations(2, true).ShouldNumber(3);
            // result == {{0, 0}, {0, 1}, {1, 1}}
        }

        [TestMethod]
        public void CombinationsTestFalse()
        {
            0.ArrayUpto(1).Combinations(2).ShouldNumber(1);
            // result == {{0, 1}}
        }

        #endregion

        #region Minimizer tests

        [TestMethod]
        public void MinimizerTest()
        {
            var doubles = new List<double> { 1.0, 2.0, 3.0, 4.0, 5.0 };
            doubles.Minimizer(d => 5.0 - d).Item1.ShouldEqual(5.0);
        }

        [TestMethod]
        public void MinimizerTestMinVal()
        {
            var doubles = new List<double> { 1.0, 2.0, 3.0, 4.0, 5.0 };
            doubles.Minimizer(d => 5.0 - d).Item2.ShouldEqual(0.0);
        }

        [TestMethod]
        public void MinimizerTestNull()
        {
            var doubles = new List<double> { 1.0, 2.0, 3.0, 4.0, 5.0 };
            doubles.Minimizer(null);
        }

        #endregion

        #endregion

        #region null handling tests

        #region EmptyIfNull tests

        [TestMethod]
        public void EmptyIfNullTest()
        {
            Assert.IsNotNull((null as IEnumerable<int>).EmptyIfNull());
        }

        [TestMethod]
        public void EmptyIfNullTestEmpty()
        {
            (null as IEnumerable<int>).EmptyIfNull().ShouldBeEmpty();
        }

        #endregion

        #endregion

        #region DistinctBy tests

        [TestMethod]
        public void Test_distinct_by_modular_arithmetic()
        {
            0.Upto(20).DistinctBy(i => i % 5).ShouldEqual(0, 1, 2, 3, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_lambda_null()
        {
            0.Upto(20).DistinctBy<int, int>(null);
        }

        #endregion

        #region string formatting tests

        #region comma separation tests

        [TestMethod]
        public void SpacedCommaSeparateTest4()
        {
            1.Upto(3).SpacedCommaSeparate().ShouldEqual("1 , 2 , 3");
        }

        [TestMethod]
        public void CommaSeparateTest()
        {
            1.Upto(3).CommaSeparate().ShouldEqual("1,2,3");
        }

        [TestMethod]
        public void SpacedAfterCommaSeparateTest()
        {
            1.Upto(3).SpacedAfterCommaSeparate().ShouldEqual("1, 2, 3");
        }

        #endregion

        #region EnglishSeparate tests

        [TestMethod]
        public void EnglishSeparateTestOne()
        {
            "a".WrapInList().EnglishSeparate().ShouldEqual("a");
        }

        [TestMethod]
        public void EnglishSeparateTestTwo()
        {
            new List<string> { "a", "b" }.EnglishSeparate().ShouldEqual("a and b");
        }

        [TestMethod]
        public void EnglishSeparateTestThree()
        {
            new List<string> { "a", "b", "c" }.EnglishSeparate().ShouldEqual("a, b and c");
        }

        [TestMethod]
        public void EnglishSeparateTestFour()
        {
            new List<string> { "a", "b", "c", "d" }.EnglishSeparate().ShouldEqual("a, b, c and d");
        }

        #endregion

        #region ToStrings tests

        [TestMethod]
        public void ToStringsTestEmpty()
        {
            new List<int>().ToStrings().ToList().ShouldBeEmpty();
        }

        [TestMethod]
        public void ToStringsTestContains()
        {
            new List<int> { 1, 2, 3 }.ToStrings().ShouldContainExactly("1", "2", "3");
        }

        #endregion

        #region EnumerableToString tests

        [TestMethod]
        public void TestEnumerableToString()
        {
            1.Upto(3).ToString("|").ShouldEqual("1|2|3");
        }

        [TestMethod]
        public void TestEnumerableToStringEmpty()
        {
            new List<int>().ToString("|").ShouldEqual(String.Empty);
        }

        [TestMethod]
        public void TestEnumerableToStringLambda()
        {
            Func<int, string> asterisk = n => "*";
            1.Upto(3).ToString(asterisk, "|").ShouldEqual("*|*|*");
        }

        #endregion

        #region ToLines tests

        [TestMethod]
        public void ToLines_should_have_correct_count()
        {
            1.Upto(10).ToLines().NumberOfLines().ShouldEqual(10);
        }

        #endregion

        #endregion

        #region sublist tests

        #region Slice tests

        [TestMethod]
        public void SliceTestCount()
        {
            1.Upto(5).Slice(2, 4).ShouldNumber(2);
        }

        [TestMethod]
        public void SliceTestCountFirst()
        {
            1.Upto(5).Slice(2, 4).First().ShouldEqual(3);
        }

        [TestMethod]
        public void SliceTestCountLast()
        {
            1.Upto(5).Slice(2, 4).Last().ShouldEqual(4);
        }

        [TestMethod]
        public void SliceTestCountNegativeStart()
        {
            1.Upto(5).Slice(-3, 4).ShouldNumber(2);
        }

        [TestMethod]
        public void SliceTestCountNegativeStart2()
        {
            1.Upto(5).Slice(-3, 4).Last().ShouldEqual(4);
        }

        [TestMethod]
        public void SliceTestCountNegativeStart3()
        {
            1.Upto(5).Slice(-3, 4).First().ShouldEqual(3);
        }

        [TestMethod]
        public void SliceTestCountNegativeEnd()
        {
            1.Upto(5).Slice(1, -2).ShouldNumber(2);
        }

        [TestMethod]
        public void SliceTestCountNegativeEnd2()
        {
            1.Upto(5).Slice(1, -2).First().ShouldEqual(2);
        }

        [TestMethod]
        public void SliceTestCountNegativeEnd3()
        {
            1.Upto(5).Slice(1, -2).Last().ShouldEqual(3);
        }

        [TestMethod]
        public void SliceTestCollection()
        {
            1.Upto(5).ToCollection().Slice(2, 4).ShouldNumber(2);
        }

        #endregion

        #region Tail tests

        [TestMethod]
        public void TailZeroElements()
        {
            1.Upto(5).Tail(0).ShouldBeEmpty();
        }

        [TestMethod]
        public void HeadZeroElements()
        {
            1.Upto(5).Head(0).ShouldBeEmpty();
        }

        [TestMethod]
        public void TailNonemptySublist()
        {
            1.Upto(5).Tail(3).ShouldNumber(3);
            1.Upto(5).ShouldContain(3, 4, 5);
        }

        [TestMethod]
        public void HeadNonemptySublist()
        {
            1.Upto(5).Tail(3).ShouldNumber(3);
            1.Upto(5).ShouldContain(3, 4, 5);
        }

        #endregion

        #endregion

        #region set-theoretic tests

        [TestMethod]
        public void ListIntersectionTest()
        {
            var lists = new List<List<int>>
            {
                1.Upto(4, n => 2 * n - 1).ToList(),
                new List<int> {1, 5, 7, 11},
                new List<int> {5, 7, 9, 11}
            };

            lists.Intersection().ShouldContainExactly(5, 7);
        }

        [TestMethod]
        public void ListIntersectionEmpty()
        {
            var lists = new List<List<int>>();
            var intersection = lists.Intersection();
            intersection.ShouldBeEmpty();
        }

        #endregion

        #region containment tests

        [TestMethod]
        public void ContainsAllTest()
        {
            1.Upto(5).ContainsAll(1, 3, 5).ShouldBeTrue();
            1.Upto(5).ContainsAll().ShouldBeTrue();
            1.Upto(5).ContainsAll(1, 3, 6).ShouldBeFalse();
        }

        [TestMethod]
        public void ContainsAnyTest()
        {
            1.Upto(5).ContainsAny(1, 3, 5).ShouldBeTrue();
            1.Upto(5).ContainsAny().ShouldBeFalse();
            1.Upto(5).ContainsAny(1, 6).ShouldBeTrue();
        }

        [TestMethod]
        public void ContainsNone()
        {
            1.Upto(5).ContainsNone(0, 6).ShouldBeTrue();
            1.Upto(5).ContainsNone(1, 3, 5).ShouldBeFalse();
            1.Upto(5).ContainsNone().ShouldBeTrue();
        }

        #endregion
    }
}