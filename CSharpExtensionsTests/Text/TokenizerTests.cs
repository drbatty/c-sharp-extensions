using CSharpExtensions.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests.Text
{
    [TestClass]
    public class TokenizerTests
    {
        [TestMethod]
        public void TestGetToken()
        {
            var t = new Tokenizer("I am a teapot");
            t.GetToken(0).ShouldEqual("I");
            t.GetToken(1).ShouldEqual("am");
            t.GetToken(2).ShouldEqual("a");
            t.GetToken(3).ShouldEqual("teapot");
            t.GetToken(-1).ShouldEqual("");
            t.GetToken(4).ShouldEqual("");
        }

        [TestMethod]
        public void TestHasToken()
        {
            var t = new Tokenizer("I am a teapot");
            t.HasToken(0).ShouldBeTrue();
            t.HasToken(1).ShouldBeTrue();
            t.HasToken(2).ShouldBeTrue();
            t.HasToken(3).ShouldBeTrue();
            t.HasToken(-1).ShouldBeFalse();
            t.HasToken(4).ShouldBeFalse();
        }

        [TestMethod]
        public void TestMatchesToken()
        {
            var t = new Tokenizer("I am a teapot");
            t.MatchesToken(0, "I").ShouldBeTrue();
            t.MatchesToken(1, "am").ShouldBeTrue();
            t.MatchesToken(2, "a").ShouldBeTrue();
            t.MatchesToken(3, "teapot").ShouldBeTrue();
        }
    }
}