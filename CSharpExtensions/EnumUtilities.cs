using System;
using System.Collections.Generic;
using System.Linq;
using CSharpExtensions.Text;

namespace CSharpExtensions
{
    public static class EnumUtilities
    {
        public static T Parse<T>(string value)
        {
            return value == null ? default(T) : Parse<T>(value, true);
        }

        public static T Parse<T>(string value, bool ignoreCase)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }

        public static bool TryParse<T>(string value, out T returnedValue)
        {
            return TryParse(value, true, out returnedValue);
        }

        public static bool TryParse<T>(string value, bool ignoreCase, out T returnedValue)
        {
            try
            {
                returnedValue = (T)Enum.Parse(typeof(T), value, ignoreCase);
                return true;
            }
            catch
            {
                returnedValue = default(T);
                return false;
            }
        }

        public static IEnumerable<T> EnumToList<T>()
        {
            var enumType = typeof(T);

            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");

            return from int val in Enum.GetValues(enumType) select (T)Enum.Parse(enumType, val.ToS());
        }

        public static IEnumerable<string> ToStrings<TEnumType>()
        {
            var enumType = typeof(TEnumType);

            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");

            return Enum.GetValues(enumType).Cast<int>().Select(e => Enum.GetName(enumType, e));
        }
    }
}
