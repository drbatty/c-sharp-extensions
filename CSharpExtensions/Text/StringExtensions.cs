using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

        public static int NumberOfLines(this string s)
        {
            return s.Split('\n').Length;
        }

        #region culture invariance

        public static string ToS(this int o)
        {
            return o.ToString(CultureInfo.InvariantCulture);
        }

        public static string ToS(this char c)
        {
            return c.ToString(CultureInfo.InvariantCulture);
        }

        public static string ToS(this double d)
        {
            return d.ToString(CultureInfo.InvariantCulture);
        }

        public static string ToS(this DateTime d)
        {
            return d.ToString(CultureInfo.InvariantCulture);
        }

        #endregion
    }
}