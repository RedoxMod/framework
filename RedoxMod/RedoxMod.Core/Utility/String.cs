using System.Text.RegularExpressions;

namespace RedoxMod.Core.Utility
{
    public static class String
    {
        private static readonly Regex _regex = new Regex("\"([^\"]+)\"|'([^']+)'|\\S+", RegexOptions.Compiled);
        public static string[] SplitQuotesStrings(this string input)
        {
            input = input.Replace("\\\"", "&qute;");
            MatchCollection matchCollection = _regex.Matches(input);

            string[] array = new string[matchCollection.Count];
            for (int i = 0; i < matchCollection.Count; i++)
            {
                array[i] = matchCollection[i].Groups[0].Value.Trim(new char[]
                {
                    ' ',
                    '"'
                });
                array[i] = array[i].Replace("&qute;", "\"");
            }
            return array;
        }      
        public static string QuoteSafe(this string str)
        {
            str = str.Replace("\"", "\\\"");
            str = str.TrimEnd(new char[]
            {
                '\\'
            });
            return "\"" + str + "\"";
        }
    }
}
