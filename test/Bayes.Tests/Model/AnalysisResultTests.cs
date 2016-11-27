using System.Collections.Immutable;
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

        [Theory]
        [InlineData(Category.Negative, "hate")]
        [InlineData(Category.Positive, "happy")]
        public void Test_Increment_Feature_Method_When_Category_No_Exist_In_Dict(Category testCategory, string feature)
        {
            var analysisResult = AnalysisResult.Empty();
            var testResult = analysisResult.IncrementFeature(testCategory, feature);
            int count;
            if (testResult.TotalFeature.TryGetValue(feature, out count))
            {
                count.Should().Be(1);
            }

            ImmutableDictionary<string, int> features;
            if (testResult.FeatureByCategory.TryGetValue(testCategory, out features))
            {
                int featuresQuantity;
                if (features.TryGetValue(feature, out featuresQuantity))
                {
                    featuresQuantity.Should().Be(1);
                }
            }
        }

        [Theory]
        [InlineData(Category.Negative, "hate")]
        [InlineData(Category.Positive, "happy")]
        public void Test_Increment_Feature_Method_When_Category_Exist_In_Dict_But_Feature_No(Category testCategory, string feature)
        {
            var analysisResult = AnalysisResult.Empty();
            var testResult = analysisResult.IncrementFeature(testCategory, "").IncrementFeature(testCategory, feature);
            int count;
            if (testResult.TotalFeature.TryGetValue(feature, out count))
            {
                count.Should().Be(1);
            }

            ImmutableDictionary<string, int> features;
            if (testResult.FeatureByCategory.TryGetValue(testCategory, out features))
            {
                int featuresQuantity;
                if (features.TryGetValue(feature, out featuresQuantity))
                {
                    featuresQuantity.Should().Be(1);
                }
            }
        }

        [Theory]
        [InlineData(Category.Negative, "hate")]
        [InlineData(Category.Positive, "happy")]
        public void Test_Increment_Feature_Method_When_Category_And_Feature_Exist(Category testCategory, string feature)
        {
            var analysisResult = AnalysisResult.Empty();
            var testResult = analysisResult.IncrementFeature(testCategory, feature).IncrementFeature(testCategory, feature);
            int count;
            if (testResult.TotalFeature.TryGetValue(feature, out count))
            {
                count.Should().Be(2);
            }

            ImmutableDictionary<string, int> features;
            if (testResult.FeatureByCategory.TryGetValue(testCategory, out features))
            {
                int featuresQuantity;
                if (features.TryGetValue(feature, out featuresQuantity))
                {
                    featuresQuantity.Should().Be(2);
                }
            }
        }
    }
}