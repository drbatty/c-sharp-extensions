using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CSharpExtensions.RegularExpressions
{
    public static class MatchCollectionExtensions
    {
        public static IEnumerable<string> ToEnumerable(this MatchCollection matchCollection)
        {
            for (var i = 0; i < matchCollection.Count; i++)
                yield return matchCollection[i].Value;
        }

        public static IList<string> ToList(this MatchCollection matchCollection)
        {
            return matchCollection.ToEnumerable().ToList();
        }
    }
}