using System.Text;
using CSharpExtensions;
using CSharpExtensions.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests.Text
{
    [TestClass]
    public class StringBuilderHtmlExtensionsTests
    {
        [TestMethod]
        public void TestAppendListItem()
        {
            new StringBuilder().Return(sB => sB.AppendListItem("list item")).ToString().ShouldEqual("<li>list item</li>");
        }

        [TestMethod]
        public void TestAppendListItemActive()
        {
            new StringBuilder().Return(sB => sB.AppendListItem("list item", true)).ToString().ShouldEqual("<li class=\"active\">list item</li>");
        }

        [TestMethod]
        public void TestAppendListItemInctive()
        {
            new StringBuilder().Return(sB => sB.AppendListItem("list item", false)).ToString().ShouldEqual("<li>list item</li>");
        }

        [TestMethod]
        public void TestAppendListItemsParams()
        {
            new StringBuilder().Return(sB => sB.AppendListItems("1", "2")).ToString().ShouldEqual("<li>1</li><li>2</li>");
        }

        [TestMethod]
        public void TestAppendUnorderedList()
        {
            new StringBuilder().Return(sB => sB.AppendUnorderedList("list contents")).ToString().ShouldEqual("<ul>list contents</ul>");
        }

        [TestMethod]
        public void TestAppendUnorderedListAction()
        {
            new StringBuilder().Return(sB => sB.AppendUnorderedList(sB2 => sB2.Append("list contents"))).ToString().ShouldEqual("<ul>list contents</ul>");
        }
    }
}