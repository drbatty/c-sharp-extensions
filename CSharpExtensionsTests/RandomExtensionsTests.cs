using System;
using CSharpExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests
{
    [TestClass]
    public class RandomExtensionsTests
    {
        [TestMethod]
        public void RandomOneOfTest()
        {
            var random = new Random();
            random.OneOf("a", "b", "c").In("a", "b", "c").ShouldBeTrue();
        }

        [TestMethod]
        public void NextBoolTest()
        {
            if (new Random().NextBool()) { }
            if (new Random().NextBool(1)) { }
        }
    }
}