using System.Text;
using CSharpExtensions.ContainerClasses;

namespace CSharpExtensions.Text
{
    public static class StringBuilderExtensions
    {
        public static void Append(this StringBuilder sB, params string[] values)
        {
            values.Each(value => sB.Append(value));
        }

        public static void AppendLines(this StringBuilder sB, params string[] values)
        {
            values.Each(value => sB.AppendLine(value));
        }
    }
}