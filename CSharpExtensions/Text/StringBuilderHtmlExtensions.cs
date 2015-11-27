using System;
using System.Text;
using CSharpExtensions.ContainerClasses;

namespace CSharpExtensions.Text
{
    public static class StringBuilderHtmlExtensions
    {
        public static void AppendListItem(this StringBuilder stringBuilder, string listItem)
        {
            stringBuilder.AppendTag("li", listItem);
        }

        public static void AppendListItem(this StringBuilder stringBuilder, string listItem, bool isActive)
        {
            if (isActive)
                stringBuilder.AppendListItem(listItem, new { @class = "active" });
            else
                stringBuilder.AppendListItem(listItem);
        }

        public static void AppendListItems(this StringBuilder stringBuilder, params string[] listItems)
        {
            listItems.Each(stringBuilder.AppendListItem);
        }

        public static void AppendListItem(this StringBuilder stringBuilder, string listItem, object attributes)
        {
            stringBuilder.AppendTag("li", listItem, attributes);
        }

        public static void AppendUnorderedList(this StringBuilder stringBuilder, Action<StringBuilder> λ)
        {
            stringBuilder.AppendOpeningTag("ul");
            λ(stringBuilder);
            stringBuilder.AppendClosingTag("ul");
        }

        public static void AppendUnorderedList(this StringBuilder stringBuilder, string listContents)
        {
            stringBuilder.AppendTag("ul", listContents);
        }
    }
}