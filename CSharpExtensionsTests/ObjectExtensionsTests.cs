using System;
using CSharpExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests
{
    [TestClass]
    public class ObjectExtensionsTests
    {
        [TestMethod]
        public void IsntTestTrue()
        {
            String.Empty.Isnt<int>().ShouldBeTrue();
        }

        [TestMethod]
        public void IsntTestFalse()
        {
            String.Empty.Isnt<string>().ShouldBeFalse();
        }
    }
}