using System.Collections.Generic;
using System.Linq;
using Bayes.Classifiers.Interfaces;
using Bayes.Data;
using Bayes.Utils;

namespace Bayes.Classifiers.Implementations
{
    public class TweetClassifier : IClassifier<string, Score>
    {
        private readonly AnalysisResult _analysisResult;

        public TweetClassifier(AnalysisResult analysisResult)
        {
            _analysisResult = analysisResult;
        }


        public Score Classify(string source)
        {
            var features = source.Tokenize();
            var probabilities = CountProbabilities(features);

            return null;
        }

        private IEnumerable<Classification> CountProbabilities(IEnumerable<string> features)
        {
            var probabilities = new SortedSet<Classification>();
            var featureList = features.ToList();
            foreach (var category in _analysisResult.TotalCategory.Keys)
            {
                probabilities.Add(new Classification(1.0, category, featureList));
            }
            return probabilities;
        }

        private Probability CountProbability(IEnumerable<string> features, Category category)
        {
            int count;
            if (_analysisResult.TotalCategory.TryGetValue(category, out count))
            {
                var featuresProbability = features.Aggregate(1.0, (acc, x) =>
                {
                    return acc * 1;
                });
                return ((double)count) / _analysisResult.TotalCategory.Values.Sum() * featuresProbability;
            }
            return 1;
        }

    }
}