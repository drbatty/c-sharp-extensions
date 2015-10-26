using System.Collections.Generic;
using CSharpExtensions;
using CSharpExtensions.ContainerClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests.Text
{
    [TestClass]
    public class StringExtensionsTests
    {
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

        [TestMethod]
        public void CommaSeparate_with_prefix_should_have_correct_format()
        {
            new List<string> { "1", "2", "3" }.CommaSeparate("v").ShouldEqual("v1,v2,v3");
        }

        [TestMethod]
        public void CommaSeparate_with_prefix_should_have_correct_format_for_empty_list()
        {
            new List<string>().CommaSeparate("v").ShouldEqual(string.Empty);
        }

        [TestMethod]
        public void CommaSeparatePrefixedWithNullTest()
        {
            IEnumerable<string> stringList = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            stringList.CommaSeparate("").ShouldEqual("");
        }

        #endregion

        #endregion
    }
}