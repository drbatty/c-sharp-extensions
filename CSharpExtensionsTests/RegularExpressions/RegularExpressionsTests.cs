using System.Linq;
using CSharpExtensions.RegularExpressions;
using CSharpExtensions.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests.RegularExpressions
{
    [TestClass]
    public class RegularExpressionsTests
    {
        [TestMethod]
        public void Hyperlink_regex_should_find_matches()
        {
            var page = @"<a href=""www.google.com"">asdfasdfadsf</a> <p>asdfasdf</p> <a href=""asdfasdf"">tretertert</a>";
            var matches = page.Matches(Regexs.Hyperlink).ToList();
            matches.ShouldNumber(2);
            matches[0].ShouldEqual(@"<a href=""www.google.com"">");
            matches[1].ShouldEqual(@"<a href=""asdfasdf"">");
        }
    }
}