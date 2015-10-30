using CSharpExtensions;
using CSharpExtensions.ContainerClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests.ContainerClasses
{
    [TestClass]
    public class CollectionExtensionsTests
    {
        [TestMethod]
        public void AddRangesTest()
        {
            var numbers = 0.Upto(5).ToCollection();
            numbers.AddRanges(6.Upto(7), 8.Upto(9));
            numbers.ShouldContainExactly(0, 1, 2, 3, 4, 5, 6, 7, 8, 9);
        }
    }
}