using System;
using System.Text;
using CSharpExtensions.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests.Text
{
    [TestClass]
    public class StringBuilderExtensionsTests
    {
        [TestMethod]
        public void TestAppendMultiple()
        {
            var sB = new StringBuilder();

            sB.Append("a", "b", "c");
            sB.ToString().ShouldEqual("abc");
        }

        [TestMethod]
        public void TestAppendNulls()
        {
            var sB = new StringBuilder();
            sB.Append("a", null, "c");
        }

        [TestMethod]
        public void TestAppendLinesMultiple()
        {
            var sB = new StringBuilder();

            sB.AppendLines("a", "b", "c");
            sB.ToString().ShouldEqual("a" + Environment.NewLine + "b" + Environment.NewLine + "c" + Environment.NewLine);
        }

        [TestMethod]
        public void TestAppendLinesNulls()
        {
            var sB = new StringBuilder();
            sB.AppendLines("a", null, "c");
        }
    }
}