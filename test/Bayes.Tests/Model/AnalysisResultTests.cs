using Bayes.Data;
using FluentAssertions;
using Xunit;

namespace Bayes.Tests.Model
{
    public class AnalysisResultTests
    {
        [Theory]
        [InlineData(Category.Negative)]
        [InlineData(Category.Positive)]
        public void Test_Increment_Category_Method_When_No_Exist_In_Dict(Category testCategory)
        {
            var analysisResult = AnalysisResult.Empty();
            var testResult = analysisResult.IncrementCategory(testCategory);
            int count;
            if (testResult.TotalCategory.TryGetValue(testCategory, out count))
            {
                count.Should().Be(1);
            }
        }

        [Theory]
        [InlineData(Category.Negative)]
        [InlineData(Category.Positive)]
        public void Test_Increment_Category_Method_When_Exist_In_Dict(Category testCategory)
        {
            var analysisResult = AnalysisResult.Empty();
            var testResult = analysisResult.IncrementCategory(testCategory).IncrementCategory(testCategory);
            int count;
            if (testResult.TotalCategory.TryGetValue(testCategory, out count))
            {
                count.Should().Be(2);
            }
        }
    }
}