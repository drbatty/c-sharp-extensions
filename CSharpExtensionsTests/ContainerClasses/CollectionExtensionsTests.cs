using System.Collections.Generic;
using System.Linq;
using CSharpExtensions;
using CSharpExtensions.ContainerClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests.ContainerClasses
{
    [TestClass]
    public class CollectionExtensionsTests
    {
        private static readonly ICollection<int> FirstFive = 1.Upto(5).ToCollection();
        private static readonly ICollection<int> SecondFive = 6.Upto(10).ToCollection();
        private static readonly ICollection<int> FirstTen = 1.Upto(10).ToCollection();

        [TestMethod]
        public void MultipleRemoveCountTest()
        {
            FirstTen.ToCollection().Return(n => n.Remove(SecondFive)).ShouldNumber(5);
        }

        [TestMethod]
        public void MultipleRemoveFirstTest()
        {
            FirstTen.ToCollection().Return(n => n.Remove(SecondFive)).First().ShouldEqual(1);
        }

        [TestMethod]
        public void MultipleRemoveLastTest()
        {
            FirstTen.ToCollection().Return(n => n.Remove(SecondFive)).Last().ShouldEqual(5);
        }

        [TestMethod]
        public void MultipleRemoveNullTest()
        {
            FirstTen.ToCollection().Remove(null as IEnumerable<int>);
        }

        [TestMethod]
        public void RemovePredicateTest()
        {
            FirstTen.ToCollection().Return(n => n.Remove(x => x < 5)).ShouldNumber(6);
        }

        [TestMethod]
        public void RemovePredicateNullTest()
        {
            FirstTen.ToCollection().Remove(null);
        }

        [TestMethod]
        public void ContainsEnumerableTest()
        {
            FirstTen.Contains(FirstFive).ShouldBeTrue();
        }

        [TestMethod]
        public void AddValuesTest()
        {
            var numbers = 1.Upto(5).ToCollection();
            numbers.AddMany(6, 7);
            numbers.ShouldNumber(7);
        }
        [TestMethod]
        public void AddRangesTest()
        {
            var numbers = 0.Upto(5).ToCollection();
            numbers.AddRanges(6.Upto(7), 8.Upto(9));
            numbers.ShouldContainExactly(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);
        }
    }
}