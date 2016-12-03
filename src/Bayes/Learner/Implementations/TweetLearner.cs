using Bayes.Data;
using Bayes.Learner.Interfaces;
using Bayes.Utils;
using System.Linq;

namespace Bayes.Learner.Implementations
{
    public class TweetLearner : ITweetLearner
    {
        public LearnerState Learn(LearnerState oldState, Sentence source)
        {
            var result = source.Text.Tokenize().Aggregate(oldState, (acc, x) =>
            {
                var newAcc = acc.IncrementFeature(source.Category, x);
                return newAcc.IncrementCategory(source.Category);
            });        
            return result;
        }
    }
}