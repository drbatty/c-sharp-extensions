using CSharpExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests
{
    [TestClass]
    public class DoubleExtensionsTests
    {
        [TestMethod]
        public void DisplayAsSumOfMoneyTest()
        {
            1.0.DisplayAsSumOfMoney().ShouldEqual("1.00");
        }

        [TestMethod]
        public void DistanceTest()
        {
            (1.0).Distance(-1.0).ShouldEqual(2.0);
            (3.0).Distance(3.0).ShouldEqual(0.0);
        }
    }
}