namespace Bayes.Data
{
    public struct Probability
    {
        public double Value { get; }

        public Probability(double value)
        {
            Value = value;
        }

        public static implicit operator Probability(double value) => new Probability(value);
        public static implicit operator double(Probability probability) => probability.Value;
    }
}