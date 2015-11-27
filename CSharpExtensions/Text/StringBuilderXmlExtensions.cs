using System.Text;

namespace CSharpExtensions.Text
{
    public static class StringBuilderXmlExtensions
    {
        public static void AppendTag(this StringBuilder stringBuilder, string tagName, string content)
        {
            stringBuilder.AppendTag(tagName, content, new { });
        }

        public static void AppendOpeningTag(this StringBuilder stringBuilder, string tagName)
        {
            stringBuilder.Append(tagName.WrapInOpeningTag());
        }

        public static void AppendClosingTag(this StringBuilder stringBuilder, string tagName)
        {
            stringBuilder.Append(tagName.WrapInClosingTag());
        }

        public static void AppendTag(this StringBuilder stringBuilder, string tagName, string content, object attributes)
        {
            stringBuilder.Append("<" + tagName);
            attributes.EachProperty(property => stringBuilder.Append(property.Name.ToLower() + "=\"" + attributes.GetPropertyValue(property.Name)));
            stringBuilder.Append(">" + content);
            stringBuilder.AppendClosingTag(tagName);
        }
    }
}
