using System.Collections.Immutable;
using Bayes.Data;
using Bayes.Learner.Implementations;
using Bayes.Trainers.Implementations;
using FluentAssertions;
using Xunit;

namespace Bayes.Tests.Trainers
{
    public class TweetTrainerTests
    {
        [Fact]
        public void Test_Tweet_Traine()
        {
            var trainer = new TweetTrainer(new TweetLearner());
            trainer.Train(new Sentence("test", WordCategory.Positive));
            trainer.Train(new Sentence("test", WordCategory.Negative));
            trainer.CurrentState.CategoryPerQuantity.Keys.Should().NotBeEmpty();
            trainer.CurrentState.CategoryPerQuantity.Keys.Should().Contain(WordCategory.Positive).And.Contain(WordCategory.Negative);
            trainer.CurrentState.WordPerQuantity.Keys.Should().NotBeEmpty();
            trainer.CurrentState.WordPerQuantity.Keys.Should().Contain("test");
            trainer.CurrentState.WordPerQuantity.GetValueOrDefault("test").Should().Be(2);
        }
    }
}