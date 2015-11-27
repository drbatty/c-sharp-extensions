using System.Linq;
using System.Reflection;
using CSharpExtensions.ContainerClasses;

namespace CSharpExtensions.Reflection
{
    public static class PropertyInfoExtensions
    {
        /// <summary>
        /// Extension which returns true if and only if a property has an attribute with the given name. 
        /// </summary>
        /// <param name="property">the PropertyInfo object to test for a given attribute name</param>
        /// <param name="attributeName">the name of the attribute to look for</param>
        /// <returns> a boolean, which is true if an only if the property has an attribute with the given name</returns>
        public static bool HasAttribute(this PropertyInfo property, string attributeName)
        {
            return property.GetCustomAttributesData().Any(attribData => attribData.NameWithoutAttribute() == attributeName);
        }

        /// <summary>
        /// Extension which retrieves the value of a property's attribute of a given name.
        /// The value must be supplied as the argument for a one-parameter constructor of the attribute,
        /// for example [Name("Name")]
        /// </summary>
        /// <param name="property">the PropertyInfo object for which to find the attribute value</param>
        /// <param name="attributeName">the name of the attribute to look for</param>
        /// <returns> the value of the attribute with the given name, or null if it doesn't exist </returns>
        public static object GetAttributeValue(this PropertyInfo property, string attributeName)
        {
            return (from attribData in property.GetCustomAttributesData()
                    where attribData.ConstructorArguments.OneOf() && attribData.NameWithoutAttribute() == attributeName
                    select attribData.ConstructorArguments[0].Value).FirstOrDefault();
        }
    }
}