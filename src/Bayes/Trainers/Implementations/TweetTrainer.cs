using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
            var state = CurrentState;
            bool isLocked = false;
            try
            {
                Monitor.Enter(state, ref isLocked);
                CurrentState = _learner.Learn(CurrentState, trainData);
            }
            finally
            {
                if (isLocked)
                {
                    Monitor.Exit(state);
                }
            }
        }

        public void Train(IEnumerable<Sentence> trainDataArray)
        {
            var state = CurrentState;
            bool isLocked = false;
            try
            {
                Monitor.Enter(state, ref isLocked);
                CurrentState = trainDataArray.Aggregate(CurrentState, (acc, x) => _learner.Learn(acc, x));
            }
            finally
            {
                if (isLocked)
                {
                    Monitor.Exit(state);
                }
            }
        }

        public LearnerState CurrentState { get; private set; }
    }
}