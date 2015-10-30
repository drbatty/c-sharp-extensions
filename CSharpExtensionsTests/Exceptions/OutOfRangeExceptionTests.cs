using CSharpExtensions.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests.Exceptions
{
    [TestClass]
    public class OutOfRangeExceptionTests
    {
        [TestMethod]
        public void TestOutOfRangeException()
        {
            new OutOfRangeException(0, 5, 6).ToString().ShouldEqual("6 IS NOT IN THE RANGE [0,5].");
        }
    }
}
