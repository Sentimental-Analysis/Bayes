using System.Collections.Generic;
using Bayes.Classifiers.Interfaces;
using Bayes.Data;

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
            var probabilities = new SortedSet<Classification>();
            var categories = _analysisResult.TotalCategory.Keys;
            return null;
        }
    }
}