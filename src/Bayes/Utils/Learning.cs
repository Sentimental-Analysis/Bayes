using System.Collections.Immutable;
using System.Linq;
using Bayes.Data;
using Bayes.Learner.Implementations;

namespace Bayes.Utils
{
    public static class Learning
    {
        public static LearnerState FromDictionary(ImmutableDictionary<string, int> source)
        {
            var learner = new TweetLearner();
            return source.Aggregate(LearnerState.Empty, (acc, x) => learner.Learn(acc,
                new Sentence(x.Key, x.Value >= 0 ? WordCategory.Positive : WordCategory.Negative)));
        }
    }
}