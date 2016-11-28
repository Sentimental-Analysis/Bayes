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
            var aprioriProbability = GetAprioriProbability();
            return null;
        }

        public ImmutableDictionary<WordCategory, Probability> GetAprioriProbability()
        {
            int allElements = _learnerState.CategoryPerQuantity.Select(x => x.Value).Sum();
            return _learnerState.CategoryPerQuantity.Keys.Select(key =>
            {
                int count = _learnerState.CategoryPerQuantity.GetValueOrDefault(key);
                return new KeyValuePair<WordCategory, Probability>(key, new Probability((double)count / allElements));
            }).ToImmutableDictionary();
        }
    }
}