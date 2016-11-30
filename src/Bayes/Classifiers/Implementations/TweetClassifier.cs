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
    public class TweetClassifier : IClassifier<Sentence, string>
    {
        private readonly LearnerState _learnerState;

        public TweetClassifier(LearnerState learnerState)
        {
            _learnerState = learnerState;
        }

        public Sentence Classify(string parameter)
        {
            var words = parameter.Tokenize();
            int allElementsQuantity = _learnerState.CategoryPerQuantity.Select(x => x.Value).Sum();
            var aprioriProbability = GetAprioriProbability(allElementsQuantity);
            var prioriProbability = GetPrioriProbability(words, allElementsQuantity);

            double positiveProbability = 0.0;
            Probability positiveAprioriProbability;
            if (aprioriProbability.TryGetValue(WordCategory.Positive, out positiveAprioriProbability))
            {
                Probability positivePrioriProbability;
                if (prioriProbability.TryGetValue(WordCategory.Positive, out positivePrioriProbability))
                {
                    positiveProbability = (positiveAprioriProbability * positivePrioriProbability).Value;
                }
            }

            double negativeProbability = 0.0;
            Probability negativeAprioriProbability;
            if (aprioriProbability.TryGetValue(WordCategory.Negative, out negativeAprioriProbability))
            {
                Probability negativePrioriProbability;
                if (prioriProbability.TryGetValue(WordCategory.Negative, out negativePrioriProbability))
                {
                    negativeProbability = (negativeAprioriProbability * negativePrioriProbability).Value;
                }
            }

            return new Sentence(parameter, (positiveAprioriProbability >= negativeAprioriProbability) ? WordCategory.Positive : WordCategory.Negative);
        }


        public ImmutableDictionary<WordCategory, Probability> GetPrioriProbability(IEnumerable<string> words, int quantity)
        {
            var prob = words.SelectMany(word => GetPrioriProbabilityForSingleWord(word, quantity)).Where(x => Math.Abs(x.Value) < Epsilon).ToList();
            var negativeProbability = prob.Where(x => x.Key == WordCategory.Negative)
                .Aggregate(new Probability(1.0d), (acc, x) => x.Value * acc);
            var positiveProbability = prob.Where(x => x.Key == WordCategory.Positive)
                .Aggregate(new Probability(1.0d), (acc, x) => x.Value * acc);
            return ImmutableDictionary<WordCategory, Probability>.Empty.Add(WordCategory.Positive, positiveProbability).Add(WordCategory.Negative, negativeProbability);
        }
        public IEnumerable<KeyValuePair<WordCategory, Probability>> GetPrioriProbabilityForSingleWord(string word, int quantity)
        {
            var categories = _learnerState.CategoryPerQuantity.Keys;
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

        public ImmutableDictionary<WordCategory, Probability> GetAprioriProbability(int allElementsQuantity)
        {
            return _learnerState.CategoryPerQuantity.Keys.Select(key =>
            {
                int count = _learnerState.CategoryPerQuantity.GetValueOrDefault(key);
                return new KeyValuePair<WordCategory, Probability>(key, new Probability((double)count / allElementsQuantity));
            }).ToImmutableDictionary();
        }
    }
}