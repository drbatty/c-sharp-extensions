using System.Reflection;

namespace CSharpExtensions.Reflection
{
    public static class CustomAttributeDataExtensions
    {
        /// <summary>
        /// Extension to return the name of an attribute by removing "Attribute" from the end of the class name.
        /// </summary>
        /// <param name="attributeData">the custom attribute data from an "attributable" code element</param>
        /// <returns> the name of the attribute obtained by truncating the attribute class name</returns>
        public static string NameWithoutAttribute(this CustomAttributeData attributeData)
        {
            if (attributeData.Constructor.DeclaringType == null)
                return null;
            var attributeTypeName = attributeData.Constructor.DeclaringType.Name;
            if (attributeTypeName.EndsWith("Attribute"))
                attributeTypeName = attributeTypeName.Substring(0, attributeTypeName.Length - 9);
            return attributeTypeName;
        }
    }
}