using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Bayes.Classifiers.Interfaces;
using Bayes.Data;
using Bayes.Utils;
using System.Linq;
using static System.Double;

namespace Bayes.Classifiers.Implementations
{
    public class TweetClassifier : IClassifier<Score, string>
    {
        private readonly LearnerState _learnerState;

        public TweetClassifier(LearnerState learnerState)
        {
            _learnerState = learnerState;
        }

        public Score Classify(string parameter)
        {
            var words = parameter.Tokenize();
            int allElementsQuantity = _learnerState.CategoryPerQuantity.Select(x => x.Value).Sum();
            var keys = _learnerState.CategoryPerQuantity.Keys.ToList();
            var aprioriProbability = GetAprioriProbability(keys, allElementsQuantity);
            var prioriProbability = GetPrioriProbability(keys, words, allElementsQuantity);

            var res = aprioriProbability.Zip(prioriProbability, (apriori, priori) =>
            {
                if (apriori.Key == priori.Key)
                {
                    double probability = 0.0;
                    Probability aprioriProbabilityValue;
                    if (aprioriProbability.TryGetValue(apriori.Key, out aprioriProbabilityValue))
                    {
                        Probability prioriProbabilityValue;
                        if (prioriProbability.TryGetValue(apriori.Key, out prioriProbabilityValue))
                        {
                            probability = (aprioriProbabilityValue * prioriProbabilityValue).Value;
                        }
                    }
                    return Tuple.Create(apriori.Key, new Probability(probability));
                }
                return Tuple.Create(apriori.Key, new Probability(-1d));
            }).MaxBy(x => x.Item2.Value);
            return new Score(new Sentence(parameter, res.Item1), res.Item2);
        }


        public ImmutableDictionary<WordCategory, Probability> GetPrioriProbability(IEnumerable<WordCategory> categories, IEnumerable<string> words, int quantity)
        {
            var prob = words.SelectMany(word => GetPrioriProbabilityForSingleWord(categories, word, quantity)).Where(x => !(Math.Abs(x.Value) < Epsilon)).ToList();
            return categories.Select(category =>
            {
                var res = prob.Where(x => x.Key == category)
                    .Aggregate(new Probability(1.0d), (acc, x) => x.Value * acc);
                return new KeyValuePair<WordCategory, Probability>(category, new Probability(Math.Abs(res.Value - 1.0d) < Epsilon ? 0.0d : res.Value));
            }).ToImmutableDictionary();
        }
        public IEnumerable<KeyValuePair<WordCategory, Probability>> GetPrioriProbabilityForSingleWord(IEnumerable<WordCategory> categories,  string word, int quantity)
        {
            return categories.Select(category =>
            {
                ImmutableDictionary<string, int> wordPerQuantity;
                if (_learnerState.CategoryPerWords.TryGetValue(category, out wordPerQuantity))
                {
                    int count = wordPerQuantity.GetValueOrDefault(word);
                    return new KeyValuePair<WordCategory, Probability>(category, new Probability((double)count / quantity));
                }
                return new KeyValuePair<WordCategory, Probability>(category, 0.0d);
            });
        }

        public ImmutableDictionary<WordCategory, Probability> GetAprioriProbability(IEnumerable<WordCategory> categories, int allElementsQuantity)
        {
            return categories.Select(key =>
            {
                int count = _learnerState.CategoryPerQuantity.GetValueOrDefault(key);
                return new KeyValuePair<WordCategory, Probability>(key, new Probability((double)count / allElementsQuantity));
            }).ToImmutableDictionary();
        }
    }
}