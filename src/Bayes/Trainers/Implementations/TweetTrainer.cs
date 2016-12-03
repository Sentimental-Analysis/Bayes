using System.Collections.Generic;
using System.Linq;
using Bayes.Data;
using Bayes.Learner.Interfaces;
using Bayes.Trainers.Interfaces;

namespace Bayes.Trainers.Implementations
{
    public class TweetTrainer : ITweetTrainer
    {
        private readonly ITweetLearner _learner;

        public TweetTrainer(ITweetLearner learner)
        {
            _learner = learner;
            CurrentState = LearnerState.Empty;
        }

        public TweetTrainer(ITweetLearner learner, LearnerState oldState)
        {
            _learner = learner;
            CurrentState = oldState;
        }

        public void Train(Sentence trainData)
        {
            CurrentState = _learner.Learn(CurrentState, trainData);
        }

        public void Train(IEnumerable<Sentence> trainDataArray)
        {
            CurrentState = trainDataArray.Aggregate(CurrentState, (acc, x) => _learner.Learn(acc, x));
        }

        public LearnerState CurrentState { get; private set; }
    }
}