using System;
using System.Collections.Generic;
using System.Linq;
using CSharpExtensions;
using CSharpExtensions.ContainerClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests.ContainerClasses
{
    [TestClass]
    public class ListExtensionsTests
    {
        [TestMethod]
        public void ListListElementAccessTest()
        {
            var lists = new List<List<int>>
            {
                1.Upto(4, n => 2 * n - 1).ToList(),
                new List<int> {1, 5, 7, 11},
                new List<int> {5, 7, 9, 11}
            };

            var list = lists.ElementAt(0);

            list.ShouldNumber(4);
            list.ElementAt(0).ShouldEqual(1);
            list.ElementAt(1).ShouldEqual(3);
            list.ElementAt(2).ShouldEqual(5);
            list.ElementAt(3).ShouldEqual(7);
        }

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

        [TestMethod]
        public void ActionAtTest()
        {
            var testString = "";
            Action<string> action = s => testString += s;
            1.Upto(10).ToStrings().ToList().ActionAt(2, action);
            testString.ShouldEqual("3");
        }

        [TestMethod]
        public void SwapTest()
        {
            var swapped = 1.Upto(2).ToList().Swap(0, 1);
            swapped[0].ShouldEqual(2);
            swapped[1].ShouldEqual(1);
        }
    }
}