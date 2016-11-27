using Bayes.Classifiers.Interfaces;
using Bayes.Data;

namespace Bayes.Classifiers.Implementations
{
    public class TweetClassifier : IClassifier<string, Score>
    {
        public Score Classify(string source)
        {
            throw new System.NotImplementedException();
        }
    }
}