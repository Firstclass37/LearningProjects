using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Tree.Extensions
{
    internal static class RegexExtentions
    {

        public static List<Match> ToList(this MatchCollection collection)
        {
            var matches = new List<Match>();
            foreach (Match match in collection)
                matches.Add(match);
            return matches;
        }
    }
}
