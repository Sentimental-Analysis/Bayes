using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Bayes.Utils
{
    public static class Text
    {
        public static IEnumerable<string> Tokenize(this string word)
        {
            return word.Split(' ').Select(x => Regex.Replace(x, @"\W", "").ToLower());
        }
    }
}
