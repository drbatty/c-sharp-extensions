using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace CSharpExtensions.Text
{
    public static class StringExtensions
    {
        public static List<Tuple<XmlNodeType, string>> FlattenXml(this string xml)
        {
            var elements = new List<Tuple<XmlNodeType, string>>();

            using (var reader = XmlReader.Create(new StringReader(xml)))
                while (reader.Read())
                {
                    var item1 = reader.NodeType;
                    var item2 = "";
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            item2 = reader.Name;
                            break;
                        case XmlNodeType.Text:
                            item2 = reader.Value;
                            break;
                        case XmlNodeType.XmlDeclaration:
                            item2 = reader.Name;
                            break;
                        case XmlNodeType.EndElement:
                            item2 = reader.Name;
                            break;
                    }
                    if (item1.In(XmlNodeType.Element, XmlNodeType.Text, XmlNodeType.XmlDeclaration,
                        XmlNodeType.EndElement))
                        elements.Add(new Tuple<XmlNodeType, string>(item1, item2));
                }

            return elements;
        }

        #region comma separation

        /// <summary>
        /// returns a string representation of a given enumerable of strings with the elements separated by commas and no spacing
        /// </summary>
        /// <typeparam name="T">the type enumerated by the enumerable</typeparam>
        /// <param name="stringList">the li</param>
        /// <returns>a string representation of a given enumerable of strings with the elements separated by commas and no spacing</returns>
        public static string CommaSeparate<T>(this IEnumerable<T> stringList)
        {
            return stringList != null ? string.Join(",", stringList) : string.Empty;
        }

        public static string SpacedCommaSeparate<T>(this IEnumerable<T> stringList)
        {
            return stringList != null ? string.Join(" , ", stringList) : string.Empty;
        }

        public static string SpacedAfterCommaSeparate<T>(this IEnumerable<T> stringList)
        {
            return stringList != null ? string.Join(", ", stringList) : string.Empty;
        }

        public static string CommaSeparate<T>(this IEnumerable<T> stringList, string prefix)
        {
            if (stringList == null)
                return string.Empty;
            var enumerable = stringList as IList<T> ?? stringList.ToList();
            var result = enumerable.Any() ? prefix : string.Empty;
            return result + string.Join("," + prefix, enumerable);
        }

        #endregion
    }
}