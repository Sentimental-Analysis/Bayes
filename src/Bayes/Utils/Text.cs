﻿using System.Collections.Generic;
using System.Collections.Immutable;
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

        public static ImmutableDictionary<string, int> Frequency(this IEnumerable<string> words)
        {
            return words.Aggregate(ImmutableDictionary<string, int>.Empty, (acc, x) =>
            {
                int freq;
                if (acc.TryGetValue(x, out freq))
                {
                    return acc.SetItem(x, freq + 1);
                }
                return acc.Add(x, 1);
            });
        }
    }
}