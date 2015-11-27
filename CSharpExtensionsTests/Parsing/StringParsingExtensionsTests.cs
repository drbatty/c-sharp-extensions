using CSharpExtensions;
using CSharpExtensions.DesignPattern.Structural.Composite;
using CSharpExtensionsTests.GeneralFixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests.Parsing
{
    [TestClass]
    public class StringParsingExtensionsTests
    {
        [TestMethod]
        public void Test_IsParenthesisMatch()
        {
            "".IsParenthesisMatch().ShouldBeTrue();
            "()".IsParenthesisMatch().ShouldBeTrue();
            "({})".IsParenthesisMatch().ShouldBeTrue();
            "{()()}".IsParenthesisMatch().ShouldBeTrue();
            "((".IsParenthesisMatch().ShouldBeFalse();
            "}}".IsParenthesisMatch().ShouldBeFalse();
            "{(})".IsParenthesisMatch().ShouldBeFalse();
            ")".IsParenthesisMatch().ShouldBeFalse();
            "(}".IsParenthesisMatch().ShouldBeFalse();
            "{)".IsParenthesisMatch().ShouldBeFalse();
        }

        [TestMethod]
        public void TestParsing()
        {
            CompositeListFixtures.TestListString.Inspect().ParseBrackets().ShouldEqual(CompositeListFixtures.TestListString);
            "a{b(c)d}e".ParseBrackets().Count().ShouldEqual(3);
        }
    }
}