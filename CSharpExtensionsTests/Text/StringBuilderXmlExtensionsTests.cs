using System.Text;
using CSharpExtensions;
using CSharpExtensions.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests.Text
{
    [TestClass]
    public class StringBuilderXmlExtensionsTests
    {
        [TestMethod]
        public void TestAppendTag()
        {
            new StringBuilder().Return(sB => sB.AppendTag("a", "hello")).ToString().ShouldEqual("<a>hello</a>");
        }

        [TestMethod]
        public void TestAppendOpeningTag()
        {
            new StringBuilder().Return(sB => sB.AppendOpeningTag("a")).ToString().ShouldEqual("<a>");
        }
    }
}