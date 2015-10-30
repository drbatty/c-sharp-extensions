using System;
using System.Collections.Generic;
using System.Globalization;
using CSharpExtensions;
using CSharpExtensions.ContainerClasses;
using CSharpExtensions.Text;
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

        #region culture invariance

        [TestMethod]
        public void ToS_should_use_invariant_culture()
        {
            1.ToS().ShouldEqual(1.ToString(CultureInfo.InvariantCulture));
            'a'.ToS().ShouldEqual('a'.ToString(CultureInfo.InvariantCulture));
            1.0.ToS().ShouldEqual(1.0.ToString(CultureInfo.InvariantCulture));
            var now = DateTime.Now;
            now.ToS().ShouldEqual(now.ToString(CultureInfo.InvariantCulture));
        }

        #endregion

        #region character handling

        [TestMethod]
        public void CharAtTest()
        {
            "abc".CharAt(0).ShouldEqual('a');
            "abc".CharAt(2).ShouldEqual('c');
        }

        #endregion
    }
}