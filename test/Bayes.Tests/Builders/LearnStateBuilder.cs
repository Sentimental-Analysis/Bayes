using System.Collections.Immutable;
using System.Runtime.InteropServices;
using Bayes.Data;
using Bayes.Learner.Implementations;
using Bayes.Utils;

namespace Bayes.Tests.Builders
{
    public class LearnStateBuilder : IBuilder<LearnerState>
    {

        private LearnerState _learnerState;

        public LearnStateBuilder()
        {
            _learnerState = LearnerState.Empty;
        }

        public LearnStateBuilder WithLearnData(ImmutableDictionary<string, int> words)
        {
            _learnerState = Learning.FromDictionary(words);
            return this;
        }

        public LearnStateBuilder WithLearnData(Sentence sentence)
        {
            var learner = new TweetLearner();
            _learnerState = learner.Learn(_learnerState, sentence);
            return this;
        }

        public LearnerState Build()
        {
            return _learnerState;
        }
    }
}