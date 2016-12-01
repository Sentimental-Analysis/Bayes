using Bayes.Data;
using Bayes.Learner.Implementations;
using Bayes.Learner.Interfaces;

namespace Bayes.FluentApi
{
    public class FluentTweetLearner
    {
        private readonly ILearner<LearnerState, Sentence> _learner;

        public FluentTweetLearner()
        {
            State = LearnerState.Empty;
            _learner = new TweetLearner();
        }

        public FluentTweetLearner Learn(Sentence sentence)
        {
            State = _learner.Learn(State, sentence);
            return this;
        }

        public LearnerState State { get; private set; }
    }
}