using System.Collections.Immutable;
using Bayes.Data;
using Xunit;
using FluentAssertions;

namespace Bayes.Tests.Model
{
    public class LearnerStateTests
    {
        [Theory]
        [InlineData(WordCategory.Negative)]
        [InlineData(WordCategory.Positive)]
        public void Test_Increment_Category_Method_When_No_Exist_In_Dict(WordCategory testCategory)
        {
            var analysisResult = LearnerState.Empty;
            var testResult = analysisResult.IncrementCategory(testCategory);
            testResult.CategoryPerQuantity.GetValueOrDefault(testCategory).Should().Be(1);
        }

        [Theory]
        [InlineData(WordCategory.Negative)]
        [InlineData(WordCategory.Positive)]
        public void Test_Increment_Category_Method_When_Exist_In_Dict(WordCategory testCategory)
        {
            var analysisResult = LearnerState.Empty;
            var testResult = analysisResult.IncrementCategory(testCategory).IncrementCategory(testCategory);
            int count;
            testResult.CategoryPerQuantity.GetValueOrDefault(testCategory).Should().Be(2);
        }

        [Theory]
        [InlineData(WordCategory.Negative, "hate")]
        [InlineData(WordCategory.Positive, "happy")]
        public void Test_Increment_Feature_Method_When_Category_No_Exist_In_Dict(WordCategory testCategory, string feature)
        {
            var analysisResult = LearnerState.Empty;
            var testResult = analysisResult.IncrementFeature(testCategory, feature);
            int count;
            testResult.WordPerQuantity.GetValueOrDefault(feature).Should().Be(1);

            ImmutableDictionary<string, int> features;
            if (testResult.CategoryPerWords.TryGetValue(testCategory, out features))
            {
                int featuresQuantity;
                if (features.TryGetValue(feature, out featuresQuantity))
                {
                    featuresQuantity.Should().Be(1);
                }
            }
        }

        [Theory]
        [InlineData(WordCategory.Negative, "hate")]
        [InlineData(WordCategory.Positive, "happy")]
        public void Test_Decrement_Feature_Method_When_Category_No_Exist_In_Dict(WordCategory testCategory, string feature)
        {
            var analysisResult = LearnerState.Empty;
            var testResult = analysisResult.DecrementFeature(testCategory, feature);
            testResult.WordPerQuantity.ContainsKey(feature).Should().BeFalse();
            testResult.CategoryPerWords.ContainsKey(testCategory).Should().BeFalse();
        }

        [Theory]
        [InlineData(WordCategory.Negative, "hate")]
        [InlineData(WordCategory.Positive, "happy")]
        public void Test_Increment_Feature_Method_When_Category_Exist_In_Dict_But_Feature_No(WordCategory testCategory, string feature)
        {
            var analysisResult = LearnerState.Empty;
            var testResult = analysisResult.IncrementFeature(testCategory, "").IncrementFeature(testCategory, feature);
            int count;
            if (testResult.WordPerQuantity.TryGetValue(feature, out count))
            {
                count.Should().Be(1);
            }

            ImmutableDictionary<string, int> features;
            if (testResult.CategoryPerWords.TryGetValue(testCategory, out features))
            {
                int featuresQuantity;
                if (features.TryGetValue(feature, out featuresQuantity))
                {
                    featuresQuantity.Should().Be(1);
                }
            }
        }

        [Theory]
        [InlineData(WordCategory.Negative, "hate")]
        [InlineData(WordCategory.Positive, "happy")]
        public void Test_Decrement_Feature_Method_When_Category_Exist_In_Dict_But_Feature_No(WordCategory testCategory, string feature)
        {
            var analysisResult = LearnerState.Empty.IncrementFeature(testCategory, "");
            var testResult = analysisResult.DecrementFeature(testCategory, feature);
            testResult.CategoryPerWords.ContainsKey(testCategory).Should().BeTrue();
            testResult.WordPerQuantity.ContainsKey(feature).Should().BeFalse();
        }

        [Theory]
        [InlineData(WordCategory.Negative, "hate")]
        [InlineData(WordCategory.Positive, "happy")]
        public void Test_Increment_Feature_Method_When_Category_And_Feature_Exist(WordCategory testCategory, string feature)
        {
            var analysisResult = LearnerState.Empty;
            var testResult = analysisResult.IncrementFeature(testCategory, feature).IncrementFeature(testCategory, feature);
            int count;
            if (testResult.WordPerQuantity.TryGetValue(feature, out count))
            {
                count.Should().Be(2);
            }

            ImmutableDictionary<string, int> features;
            if (testResult.CategoryPerWords.TryGetValue(testCategory, out features))
            {
                int featuresQuantity;
                if (features.TryGetValue(feature, out featuresQuantity))
                {
                    featuresQuantity.Should().Be(2);
                }
            }
        }

        [Theory]
        [InlineData(WordCategory.Negative, "hate")]
        [InlineData(WordCategory.Positive, "happy")]
        public void Test_Decrement_Feature_Method_When_Category_Exist_In_Dict_And_Feature_Exist_And_Feature_Count_Is_1(WordCategory testCategory, string feature)
        {
            var analysisResult = LearnerState.Empty.IncrementFeature(testCategory, feature);
            var testResult = analysisResult.DecrementFeature(testCategory, feature);
            testResult.CategoryPerWords.ContainsKey(testCategory).Should().BeTrue();
            testResult.WordPerQuantity.ContainsKey(feature).Should().BeFalse();
        }

        [Theory]
        [InlineData(WordCategory.Negative, "hate")]
        [InlineData(WordCategory.Positive, "happy")]
        public void Test_Decrement_Feature_Method_When_Category_Exist_In_Dict_And_Feature_Exist_And_Feature_Count_Is_Greater_Than_1(WordCategory testCategory, string feature)
        {
            var analysisResult = LearnerState.Empty.IncrementFeature(testCategory, feature).IncrementFeature(testCategory, feature);
            var testResult = analysisResult.DecrementFeature(testCategory, feature);
            testResult.CategoryPerWords.ContainsKey(testCategory).Should().BeTrue();
            testResult.WordPerQuantity.ContainsKey(feature).Should().BeTrue();
            testResult.WordPerQuantity.GetValueOrDefault(feature).Should().Be(1);
        }
    }
}