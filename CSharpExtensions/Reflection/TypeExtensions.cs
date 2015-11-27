using System;
using System.Linq;
using CSharpExtensions.Text;

namespace CSharpExtensions.Reflection
{
    public static class TypeExtensions
    {
        public static bool HasAttribute(this Type type, string attributeName)
        {
            return Attribute.GetCustomAttributes(type).Select(t => t.ToString().LastToken('.')).Contains(attributeName);
        }
    }
}