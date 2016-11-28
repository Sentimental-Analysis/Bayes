using Bayes.Data;
using Bayes.Learner.Interfaces;
using Bayes.Utils;
using System.Linq;

namespace Bayes.Learner.Implementations
{
    public class TweetLearner : ILearner<LearnerState, Sentence>
    {
        public LearnerState Learn(LearnerState oldState, Sentence source)
        {
            var result = source.Text.Tokenize().Aggregate(oldState, (acc, x) => acc.IncrementFeature(source.Category, x));
            result = result.IncrementCategory(source.Category);
            return result;
        }
    }
}