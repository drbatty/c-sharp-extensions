using System.Collections.Generic;

namespace CSharpExtensionsTests.GeneralFixtures
{
    public class DictionaryFixtures
    {
        public static readonly Dictionary<string, int> Dict = new Dictionary<string, int>
        {
            {"a", 1},
            {"b", 7},
            {"c", 9}
        };

        public static Dictionary<string, string> SimpleDict = new Dictionary<string, string>
        {
            {"a", "x"},
            {"b", "y"},
            {"c", "z"}
        };
    }
}