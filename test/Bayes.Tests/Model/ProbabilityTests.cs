using System.Collections.Generic;
using Bayes.Data;
using Xunit;
using FluentAssertions;

namespace Bayes.Tests.Model
{
    public class ProbabilityTests
    {
        [Fact]
        public void Count_Pobability_When_All_Words_Are_Negative()
        {
            var dict = new Dictionary<string, int>()
            {
                {"abandon", -2 },
                {"abandoned", -2 }
            };

            var probability = Probability.Count(dict);
            probability.PositiveProbability.Should().BeLessOrEqualTo(double.Epsilon);
            probability.NegativeProbability.Should().BeLessOrEqualTo(1);
        }

        [Fact]
        public void Count_Pobability_When_All_Words_Are_Positive()
        {
            var dict = new Dictionary<string, int>()
            {
                {"exuberant", 4 },
                {"fun", 4 }
            };

            var probability = Probability.Count(dict);
            probability.NegativeProbability.Should().BeLessOrEqualTo(double.Epsilon);
            probability.PositiveProbability.Should().BeLessOrEqualTo(1);
        }

        [Fact]
        public void Count_Pobability()
        {
            var dict = new Dictionary<string, int>()
            {
                {"exuberant", 4 },
                {"fun", 4 },
                {"abandon", -2 },
                {"abandoned", -2 }
            };

            var probability = Probability.Count(dict);
            probability.NegativeProbability.Should().BeLessOrEqualTo(0.5);
            probability.PositiveProbability.Should().BeLessOrEqualTo(0.5);
        }
    }
}
