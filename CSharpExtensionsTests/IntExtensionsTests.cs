using System;
using System.Linq;
using CSharpExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests
{
    [TestClass]
    public class IntExtensionsTests
    {
        [TestMethod]
        public void UpToTestCount()
        {
            1.Upto(5).ShouldNumber(5);
        }

        [TestMethod]
        public void UpToTestCount2()
        {
            6.Upto(11).ShouldNumber(6);
        }

        [TestMethod]
        public void UptoSequenceCountTest()
        {
            1.Upto(5, n => 2 * n - 1).ShouldNumber(5);
        }

        [TestMethod]
        public void UptoSequenceValuesTest()
        {
            1.Upto(5, n => 2 * n - 1).Sum().ShouldEqual(25);
        }

        [TestMethod]
        public void UptoSequenceNullTest()
        {
            1.Upto(5, null);
        }

        [TestMethod]
        public void DoUptoTest()
        {
            var total = 0;
            1.DoUpto(5, i => total++);
            total.ShouldEqual(5);
        }

        [TestMethod]
        public void DoUptoTestNull()
        {
            1.DoUpto(5, null);
        }

        [TestMethod]
        public void DoZeroUptoTest()
        {
            var total = 0;
            5.DoZeroUpto(i => total++);
            total.ShouldEqual(6);
        }

        [TestMethod]
        public void DoTest()
        {
            var total = 0;
            5.Do(i => total++);
            total.ShouldEqual(5);
        }

        [TestMethod]
        public void DoTriangularTest()
        {
            var total = 0;
            Action<int, int> increment = (m, n) => total++;
            3.DoTriangular(increment);
            total.ShouldEqual(6);
        }

        [TestMethod]
        public void Do_on_pairs_should_give_expected_results()
        {
            var total = 0;
            Action<int, int> addProduct = (i, j) => total += i * j;
            3.Do(addProduct);
            total.ShouldEqual(9);
        }

        [TestMethod]
        public void Any_int_extension_should_give_expected_results()
        {
            3.Any(n => n == 3).ShouldBeFalse();
            3.Any(n => n == 2).ShouldBeTrue();
            3.Any(n => n == 0).ShouldBeTrue();
            3.Any(n => n == 1).ShouldBeTrue();
        }

        [TestMethod]
        public void Any_int_extension_with_start_should_give_expected_results()
        {
            1.Any(3, n => n == 3).ShouldBeFalse();
            1.Any(3, n => n == 2).ShouldBeTrue();
            1.Any(3, n => n == 0).ShouldBeFalse();
            1.Any(3, n => n == 1).ShouldBeTrue();
        }

        [TestMethod]
        public void All_int_extension_should_give_expected_results()
        {
            3.All(n => n < 3).ShouldBeTrue();
            3.All(n => n > 1).ShouldBeFalse();
            3.All(n => n >= 0).ShouldBeTrue();
        }

        [TestMethod]
        public void All_int_extension_with_start_should_give_expected_results()
        {
            1.All(3, n => n < 3).ShouldBeTrue();
            1.All(3, n => n > 1).ShouldBeFalse();
            1.All(3, n => n >= 1).ShouldBeTrue();
        }

        [TestMethod]
        public void TimesTest()
        {
            var total = 0;
            2.Times(
                () => total++
                );
            total.ShouldEqual(2);
        }

        [TestMethod]
        public void TimesTestNull()
        {
            2.Times(null);
        }
    }
}