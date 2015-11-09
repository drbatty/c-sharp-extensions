using CSharpExtensions;
using CSharpExtensions.ContainerClasses;
using CSharpExtensions.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests.Text
{
    [TestClass]
    public class CharExtensionsTests
    {
        [TestMethod]
        public void IsLowerCaseAlphabeticTestTrue()
        {
            97.Upto(122).Each(n => ((char)n).IsLowerCaseAlphabetic().ShouldBeTrue());
        }

        [TestMethod]
        public void IsLowerCaseAlphabeticTestFalse()
        {
            32.Upto(96).Each(n => ((char)n).IsLowerCaseAlphabetic().ShouldBeFalse());
            123.Upto(127).Each(n => ((char)n).IsLowerCaseAlphabetic().ShouldBeFalse());
        }

        [TestMethod]
        public void IsUpperCaseAlphabeticTestTrue()
        {
            65.Upto(90).Each(n => ((char)n).IsUpperCaseAlphabetic().ShouldBeTrue());
        }

        [TestMethod]
        public void IsUpperCaseAlphabeticTestFalse()
        {
            32.Upto(64).Each(n => ((char)n).IsUpperCaseAlphabetic().ShouldBeFalse());
            91.Upto(127).Each(n => ((char)n).IsUpperCaseAlphabetic().ShouldBeFalse());
        }

        [TestMethod]
        public void IsDigitTestTrue()
        {
            48.Upto(57).Each(n => ((char)n).IsDigit().ShouldBeTrue());
        }

        [TestMethod]
        public void IsDigitTestFalse()
        {
            32.Upto(47).Each(n => ((char)n).IsDigit().ShouldBeFalse());
            58.Upto(127).Each(n => ((char)n).IsDigit().ShouldBeFalse());
        }

        [TestMethod]
        public void IsDigitTestMaxFalse()
        {
            48.Upto(57).Each(n => ((char)n).IsDigit(-1).ShouldBeFalse());
        }

        [TestMethod]
        public void IsDigitTestMaxTrue()
        {
            48.Upto(57).Each(n => ((char)n).IsDigit(10).ShouldBeTrue());
            48.Upto(57).Each(n => ((char)n).IsDigit(9).ShouldBeTrue());
        }

        [TestMethod]
        public void IsDigitTestMaxFalse2()
        {
            32.Upto(47).Each(n => ((char)n).IsDigit(10).ShouldBeFalse());
            58.Upto(127).Each(n => ((char)n).IsDigit(9).ShouldBeFalse());
        }

        [TestMethod]
        public void IsDigitTestMax5()
        {
            32.Upto(47).Each(n => ((char)n).IsDigit(5).ShouldBeFalse());
            58.Upto(127).Each(n => ((char)n).IsDigit(5).ShouldBeFalse());
            54.Upto(57).Each(n => ((char)n).IsDigit(5).ShouldBeFalse());
            48.Upto(53).Each(n => ((char)n).IsDigit(5).ShouldBeTrue());
        }

        [TestMethod]
        public void IsLowerCaseAlphabeticTestMaxTrue()
        {
            97.Upto(122).Each(n => ((char)n).IsLowerCaseAlphabetic().ShouldBeTrue());
        }

        [TestMethod]
        public void TestNegativeMax()
        {
            'a'.IsLowerCaseAlphabetic(-1).ShouldBeFalse();
            'A'.IsLowerCaseAlphabetic(-1).ShouldBeFalse();
            'a'.IsUpperCaseAlphabetic(-1).ShouldBeFalse();
            'A'.IsUpperCaseAlphabetic(-1).ShouldBeFalse();
        }

        [TestMethod]
        public void Test26()
        {
            'z'.IsLowerCaseAlphabetic(27).ShouldBeTrue();
            'Z'.IsUpperCaseAlphabetic(27).ShouldBeTrue();
        }

        [TestMethod]
        public void TestIsAlphabetic()
        {
            'A'.IsAlphabetic().ShouldBeTrue();
            'Z'.IsAlphabetic().ShouldBeTrue();
            'a'.IsAlphabetic().ShouldBeTrue();
            'z'.IsAlphabetic().ShouldBeTrue();
            '1'.IsAlphabetic().ShouldBeFalse();
        }
    }
}
