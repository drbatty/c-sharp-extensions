using System;
using CSharpExtensions.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests.Reflection
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class PropertyInfoTestAttribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class NameAttribute : Attribute
    {
        public NameAttribute(string name)
        {
        }
    }

    [TestClass]
    public class PropertyInfoExtensionsTests
    {
        [PropertyInfoTest]
        public int PropertyInfoTest { get; set; }

        [TestMethod]
        public void HasAttributeTestTrue()
        {
            GetType().GetProperty("PropertyInfoTest").HasAttribute("PropertyInfoTest").ShouldBeTrue();
        }

        [Name("Bob")]
        public string BobName { get; set; }

        [TestMethod]
        public void HasAttributeTestFalse()
        {
            GetType().GetProperty("PropertyInfoTest").HasAttribute("Asdfgfsdfgdsfg").ShouldBeFalse();
        }

        [TestMethod]
        public void GetAttributeValueTest()
        {
            GetType().GetProperty("BobName").GetAttributeValue("Name").ToString().ShouldEqual("Bob");
        }
    }
}