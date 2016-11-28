using Bayes.Data;
using Bayes.Learner.Implementations;
using Bayes.Utils;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Bayes.Tests.Learners
{
    public class TweetLearnerTests
    {
        [Theory]
        [InlineData(WordCategory.Positive, "Wither 3 is best game ever")]
        public void Test_Learn_Method_When_Analysis_Result_Is_Empty(WordCategory category, string feature)
        {
            var analysisResult = LearnerState.Empty;
            var classification = new Sentence(feature, category);
            var teacher = new TweetLearner();
            var testResult = teacher.Learn(analysisResult, classification);

            int count;
            if (testResult.CategoryPerQuantity.TryGetValue(category, out count))
            {
                count.Should().Be(1);
            }

            testResult.WordPerQuantity.Count.Should().Be(classification.Text.Tokenize().Count());
        }
    }
}