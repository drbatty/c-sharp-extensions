using System;
using System.Collections.Generic;
using System.Linq;
using CSharpExtensions;
using CSharpExtensions.ContainerClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests.ContainerClasses
{
    [TestClass]
    public class TupleExtensionsTests
    {
        [TestMethod]
        public void PairTestFirst()
        {
            var abc = new List<string> { "a", "b", "c" };
            var def = new List<string> { "d", "e", "f", "g" };
            abc.Pair(def).Item1.ShouldNumber(3);
        }

        [TestMethod]
        public void PairTestSecond()
        {
            var abc = new List<string> { "a", "b", "c" };
            var def = new List<string> { "d", "e", "f", "g" };
            abc.Pair(def).Item2.ShouldNumber(4);
        }

        private static Tuple<List<int>, List<int>> IntsPair()
        {
            var ints1 = 1.Upto(3).ToList();
            var ints2 = 4.Upto(6).ToList();
            return ints1.Pair(ints2);
        }

        [TestMethod]
        public void DoubleEachTest()
        {
            var total = 0;
            IntsPair().Each((a, b) => total += a * b);

            total.ShouldEqual(90);
        }

        [TestMethod]
        public void DoubleSelectTestCount()
        {
            IntsPair().Select((a, b) => a + b).ShouldNumber(9);
        }

        [TestMethod]
        public void DoubleSelectTestCommutesWithDoubleEach()
        {
            var selected = IntsPair().Select((a, b) => a * b);
            var enumerable = selected as IList<int> ?? selected.ToList();
            enumerable.Sum().ShouldEqual(90);
        }

        [TestMethod]
        public void DoubleAllTest()
        {
            1.Upto(3).ToList().Pair(1.Upto(3).ToList()).All((x, y) => x * y < 10 && x * y > 0).ShouldBeTrue();
        }
    }
}
