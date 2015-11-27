using System;
using System.Linq;
using System.Reflection;
using CSharpExtensions.ContainerClasses;
using CSharpExtensions.Text;

namespace CSharpExtensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Extension which returns a parsing of the target object to an integer, if it can be done,
        /// or zero = default(int) otherwise
        /// </summary>
        /// <param name="target">the object to parse into an integer</param>
        /// <returns> an integer parsing of the target object, if it can be done, or zero = default(int) otherwise</returns>
        public static int IntOrDefault(this object target)
        {
            var valueToReturn = default(int);
            var parseValue = target != null ? target.ToString() : valueToReturn.ToS();
            int.TryParse(parseValue, out valueToReturn);
            return valueToReturn;
        }

        /// <summary>
        /// Extension which retrieves the value of a property of an object, using reflection
        /// </summary>
        /// <param name="obj">the object whose property value to retrieve</param>
        /// <param name="name">the name of the property whose value to retrieve</param>
        /// <returns> the value of the property with the supplied name, or null if it does not exist</returns>
        public static object GetPropertyValue(this object obj, string name)
        {
            return name == null ? null :
                name.Split('.').Select(part => obj.GetType()
                .GetProperty(part)).
                Select(info => info == null ? null : info.GetValue(obj, null))
                .FirstOrDefault();
        }

        /// <summary>
        /// Extension to apply an action to each property of the supplied object.
        /// </summary>
        /// <param name="o">the object whose properties to loop over</param>
        /// <param name="λ">the action to apply to each property of the object</param>
        public static void EachProperty(this object o, Action<PropertyInfo> λ)
        {
            o.GetType().GetProperties().Each(λ);
        }

        public static string Inspect(this object o)
        {
            return "{" + string.Join("," + Environment.NewLine, o.GetType().GetProperties().Select(p => p + "->" + p.GetValue(o))) + "}";
        }

        /// <summary>
        /// returns true if an object is not of the given type
        /// </summary>
        /// <typeparam name="T">the given type</typeparam>
        /// <param name="o">the given object</param>
        /// <returns>true if an object is not of the given type</returns>
        public static bool Isnt<T>(this object o)
        {
            return !(o is T);
        }
    }
}