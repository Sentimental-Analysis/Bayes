using System.Linq;
using Bayes.Utils;
using FluentAssertions;
using Xunit;

namespace Bayes.Tests.Utils
{
    public class TextUtilsTests
    {
        [Fact]
        public void Test_Frequency_Method()
        {
            var words = new[] {"1", "2", "2", "3", "3", "3"};
            var testResult = words.AsEnumerable().Frequency();
            int freq;
            if (testResult.TryGetValue("3", out freq))
            {
                freq.Should().Be(3);
            }
        }
    }
}