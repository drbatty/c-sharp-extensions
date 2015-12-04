using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests
{
    [TestClass]
    public class TestUtilTests
    {
        [TestMethod]
        public void TestShouldBeNull()
        {
            object o = null;
            o.ShouldBeNull();
        }

        #region containment tests

        [TestMethod]
        public void TestShouldNotContainItem()
        {
            new List<string>().ShouldNotContain("");
        }

        [TestMethod]
        public void TestShouldNotContainList()
        {
            new List<string>{"a"}.ShouldNotContain(new List<string>{"a", "b"});
        }

        [TestMethod]
        public void TestShouldNotContainParams()
        {
            new List<string> {"a"}.ShouldNotContain("a", "b");
        }

        #endregion

        #region numerical comparison tests

        [TestMethod]
        public void TestShouldBeAtLeast()
        {
            3.ShouldBeAtLeast(3);
            4.ShouldBeAtLeast(3);
        }

        [TestMethod]
        public void TestShouldBeAtMost()
        {
            3.ShouldBeAtLeast(3);
            2.ShouldBeAtMost(3);
        }

        [TestMethod]
        public void TestShouldBeLessThan()
        {
            2.ShouldBeLessThan(3);
        }

        [TestMethod]
        public void TestShouldBeMoreThan()
        {
            3.ShouldBeMoreThan(2);
        }

        [TestMethod]
        public void TestShouldBeBetween()
        {
            3.ShouldBeBetween(2, 4);
            2.ShouldBeBetween(2, 4);
            4.ShouldBeBetween(2, 4);
        }

        [TestMethod]
        public void TestShouldBeStrictlyBetween()
        {
            3.ShouldBeStrictlyBetween(2,4);
        }

        #endregion

        [TestMethod]
        public void TestShouldHaveType()
        {
            "".ShouldHaveType<string>();
        }
    }
}
