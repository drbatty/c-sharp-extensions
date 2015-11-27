using System;
using CSharpExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests
{
    [TestClass]
    public class ObjectExtensionsTests
    {
        [TestMethod]
        public void IsntTestTrue()
        {
            String.Empty.Isnt<int>().ShouldBeTrue();
        }

        [TestMethod]
        public void IsntTestFalse()
        {
            String.Empty.Isnt<string>().ShouldBeFalse();
        }

        [TestMethod]
        public void GetPropertiesStringTestNonexistent()
        {
            "a".GetPropertyValue("hello");
        }

        [TestMethod]
        public void GetPropertiesStringTestNull()
        {
            "a".GetPropertyValue(null);
        }

        [TestMethod]
        public void GetPropertiesTest()
        {
            var anon = new { Michael = "batty" };
            anon.GetPropertyValue("Michael").ShouldEqual("batty");
        }

        [TestMethod]
        public void EachPropertyTest()
        {
            var anon = new { One = 1, Two = 2, Three = 3 };
            var total = 0;
            anon.EachProperty(prop => total++);
            total.ShouldEqual(3);
        }

        [TestMethod]
        public void IntOrDefaultTest()
        {
            "1".IntOrDefault().ShouldEqual(1);
        }

        [TestMethod]
        public void IntOrDefaultTestGarbage()
        {
            "asdfsdafsf".IntOrDefault().ShouldEqual(0);
        }

        [TestMethod]
        public void TestInspect()
        {
            new { a = "a", b = "b" }.Inspect().ShouldEqual("{System.String a->a," + Environment.NewLine + "System.String b->b}");
        }
    }

    class TestType
    {
        public int Int { get; set; }
        public string Str { get; set; }
        public string Str2 { get; set; }
    }
}