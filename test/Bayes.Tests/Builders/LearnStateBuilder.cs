using System.Collections.Generic;
using System.Linq;
using Bayes.Data;
using Bayes.Learner.Implementations;
using Bayes.Learner.Interfaces;

namespace Bayes.Tests.Builders
{
    public class LearnStateBuilder : IBuilder<LearnerState>
    {

        private LearnerState _learnerState;
        private readonly ITweetLearner _learner;

        public LearnStateBuilder LearnState => new LearnStateBuilder();

        public LearnStateBuilder()
        {
            _learnerState = LearnerState.Empty;
            _learner = new TweetLearner();
        }

        public LearnStateBuilder WithLearnData(IEnumerable<Sentence> sentences)
        {
            _learnerState = sentences.Aggregate(_learnerState, (acc, x) => _learner.Learn(acc, x));
            return this;
        }

        public LearnStateBuilder WithLearnData(Sentence sentence)
        {
            _learnerState = _learner.Learn(_learnerState, sentence);
            return this;
        }

        public LearnerState Build()
        {
            return _learnerState;
        }
    }
}