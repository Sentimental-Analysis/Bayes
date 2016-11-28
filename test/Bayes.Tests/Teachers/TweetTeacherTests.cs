using System.Linq;
using Bayes.Data;
using Bayes.Teachers.Implementations;
using FluentAssertions;
using Xunit;

namespace Bayes.Tests.Teachers
{
    public class TweetTeacherTests
    {
        [Theory]
        [InlineData(Category.Positive, "Wither 3 is best game ever")]
        public void Test_Learn_Method_When_Analysis_Result_Is_Empty(Category category, string feature)
        {
            var analysisResult = AnalysisResult.Empty();
            var classification = new Classification(category, feature);
            var teacher = new TweetTeacher();
            var testResult = teacher.Learn(analysisResult, classification);

            int count;
            if (testResult.TotalCategory.TryGetValue(category, out count))
            {
                count.Should().Be(1);
            }

            testResult.TotalFeature.Count.Should().Be(classification.Words.Count());
        }
    }
}