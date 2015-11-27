using System;
using System.Linq;
using CSharpExtensions.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpExtensionsTests.Reflection
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class CustomAttributeDataTestAttribute : Attribute
    {
    }

    [TestClass]
    public class CustomAttributeDataExtensionsTests
    {
        [CustomAttributeDataTest] // NB Test attribute only
        public int CustomAttributeDataTest { get; set; }

        [TestMethod]
        public void NameWithoutAttributeTest()
        {
            var propertyInfo = GetType().GetProperty("CustomAttributeDataTest");
            var customAttributeData = propertyInfo.GetCustomAttributesData().FirstOrDefault(
                attribData => attribData.Constructor.DeclaringType.Name == "CustomAttributeDataTestAttribute");
            customAttributeData.NameWithoutAttribute().ShouldEqual("CustomAttributeDataTest");
        }
    }
}