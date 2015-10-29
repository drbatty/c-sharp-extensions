using System;
using CSharpExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests
{
    [TestClass]
    public class BoolExtensionsTests
    {
        [TestMethod]
        public void IfTestTrue()
        {
            var total = 0;
            true.If(0, t => total++, t => total--);
            total.ShouldEqual(1);
        }

        [TestMethod]
        public void IfTestFalse()
        {
            var total = 0;
            false.If(0, t => total++, t => total--);
            total.ShouldEqual(-1);
        }

        [TestMethod]
        public void IfTestTrue2()
        {
            var total = 0;
            true.If(() => total++);
            total.ShouldEqual(1);
        }

        [TestMethod]
        public void IfTestFalse2()
        {
            var total = 0;
            false.If(() => total++);
            total.ShouldEqual(0);
        }

        [TestMethod]
        public void IfNotTestTrue()
        {
            var total = 0;
            true.IfNot(() => total++);
            total.ShouldEqual(0);
        }

        [TestMethod]
        public void IfNotTestFalse()
        {
            var total = 0;
            false.IfNot(() => total++);
            total.ShouldEqual(1);
        }

        [TestMethod]
        public void WhenTrueTestFuncTrue()
        {
            var ten = (Func<int>)(() => 10);
            var result = true.WhenTrue(ten);
            result.ShouldEqual(10);
        }

        [TestMethod]
        public void WhenTrueTestFuncFalse()
        {
            var ten = (Func<int>)(() => 10);
            var result = false.WhenTrue(ten);
            result.ShouldEqual(0);
        }

        [TestMethod]
        public void WhenTrueTestTrue()
        {
            var result = true.WhenTrue(10);
            result.ShouldEqual(10);
        }

        [TestMethod]
        public void WhenTrueTestFalse()
        {
            var result = false.WhenTrue(10);
            result.ShouldEqual(0);
        }

        [TestMethod]
        public void WhenFalseTestFuncTrue()
        {
            var ten = (Func<int>)(() => 10);
            var result = true.WhenFalse(ten);
            result.ShouldEqual(0);
        }

        [TestMethod]
        public void WhenFalseTestFuncFalse()
        {
            var ten = (Func<int>)(() => 10);
            var result = false.WhenFalse(ten);
            result.ShouldEqual(10);
        }

        [TestMethod]
        public void WhenFalseTestTrue()
        {
            var result = true.WhenFalse(10);
            result.ShouldEqual(0);
        }

        [TestMethod]
        public void WhenFalseTestFalse()
        {
            var result = false.WhenFalse(10);
            result.ShouldEqual(10);
        }

        [TestMethod]
        public void To_Yes_Or_No_Should_Return_Correct_Values()
        {
            true.ToYesOrNo().ShouldEqual("yes");
            false.ToYesOrNo().ShouldEqual("no");
        }
    }
}
