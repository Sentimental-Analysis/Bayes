using System.Collections.Generic;
using System.Linq;

namespace Bayes.Data
{
    public struct Probability
    {
        public double NegativeProbability { get; }
        public double PositiveProbability { get; }

        public Probability(double negativeProbability, double positiveProbability)
        {
            NegativeProbability = negativeProbability;
            PositiveProbability = positiveProbability;
        }

        public static Probability Count(Dictionary<string, int> words)
        {
            var all = words.Count + 0.0;
            var negative = words.Count(x => x.Value < 0);
            var positive = all - negative;
            return new Probability(negative / all, positive / all);
        }
    }
}