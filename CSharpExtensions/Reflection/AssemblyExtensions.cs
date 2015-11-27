using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CSharpExtensions.Reflection
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetTypesInNamespace(this Assembly assembly, string nameSpace)
        {
            return (from type in assembly.GetTypes() where type.Namespace == nameSpace select type).ToList();
        }

        public static IEnumerable<Type> GetTypesWithAttribute(this Assembly assembly, string attributeName)
        {
            return (from type in assembly.GetTypes() where type.HasAttribute(attributeName) select type).ToList();
        }
    }
}
