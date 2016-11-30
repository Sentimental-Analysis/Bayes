using System.Collections.Generic;
using System.Collections.Immutable;
using Bayes.Classifiers.Interfaces;
using Bayes.Data;
using Bayes.Utils;
using System.Linq;

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
            var aprioriProbability = GetAprioriProbability();
            return null;
        }


        public ImmutableDictionary<WordCategory, Probability> GetPrioriProbability(IEnumerable<string> words, int quantity)
        {
            var res = words.Select(word => GetPrioriProbabilityForSingleWord(word, quantity));
            return null;
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
                return new KeyValuePair<WordCategory, Probability>(category, 0.0);
            });
        }

        public ImmutableDictionary<WordCategory, Probability> GetAprioriProbability()
        {
            int allElementsQuantity = _learnerState.CategoryPerQuantity.Select(x => x.Value).Sum();
            return _learnerState.CategoryPerQuantity.Keys.Select(key =>
            {
                int count = _learnerState.CategoryPerQuantity.GetValueOrDefault(key);
                return new KeyValuePair<WordCategory, Probability>(key, new Probability((double)count / allElementsQuantity));
            }).ToImmutableDictionary();
        }
    }
}