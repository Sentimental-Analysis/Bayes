using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Bayes.Data;
using static System.Double;

namespace Bayes.Utils
{
    public static class ProbabilityExtensions
    {
        public static ImmutableDictionary<WordCategory, Probability> GetPrioriProbability(IEnumerable<WordCategory> categories, IEnumerable<string> words, int quantity, LearnerState learnerState)
        {
            var prob = words.SelectMany(word => GetPrioriProbabilityForSingleWord(categories, word, quantity, learnerState)).Where(x => !(Math.Abs(x.Value) < Epsilon)).ToList();
            return categories.Select(category =>
            {
                var res = prob.Where(x => x.Key == category)
                    .Aggregate(new Probability(1.0d), (acc, x) => x.Value * acc);
                return new KeyValuePair<WordCategory, Probability>(category, new Probability(Math.Abs(res.Value - 1.0d) < Epsilon ? 0.0d : res.Value));
            }).ToImmutableDictionary();
        }

        public static IEnumerable<KeyValuePair<WordCategory, Probability>> GetPrioriProbabilityForSingleWord(IEnumerable<WordCategory> categories,  string word, int quantity, LearnerState learnerState)
        {
            return categories.Select(category =>
            {
                ImmutableDictionary<string, int> wordPerQuantity;
                if (learnerState.CategoryPerWords.TryGetValue(category, out wordPerQuantity))
                {
                    int count = wordPerQuantity.GetValueOrDefault(word);
                    return new KeyValuePair<WordCategory, Probability>(category, new Probability((double)count / quantity));
                }
                return new KeyValuePair<WordCategory, Probability>(category, 0.0d);
            });
        }

        public static ImmutableDictionary<WordCategory, Probability> GetAprioriProbability(IEnumerable<WordCategory> categories, int allElementsQuantity, LearnerState learnerState)
        {
            return categories.Select(key =>
            {
                int count = learnerState.CategoryPerQuantity.GetValueOrDefault(key);
                return new KeyValuePair<WordCategory, Probability>(key, new Probability((double)count / allElementsQuantity));
            }).ToImmutableDictionary();
        }
    }
}