using System;

namespace Bayes.Data
{
    public struct Probability
    {
        public double Value { get; }

        public Probability(double value)
        {
            Value = value;
        }

        public static implicit operator double(Probability probability) => probability.Value;
        public static implicit operator Probability(double value) => new Probability(value);

        public static Probability operator *(Probability probability1, Probability probability2)
        {
            return new Probability(probability1.Value * probability2.Value);
        }

        public static bool operator ==(Probability probability, Probability probability1)
        {
            return Math.Abs(probability.Value - probability1.Value) < double.Epsilon;
        }

        public static bool operator !=(Probability probability, Probability probability1)
        {
            return !(probability == probability1);
        }

        public static bool operator >(Probability probability, Probability probability1)
        {
            return probability.Value > probability1.Value;
        }

        public static bool operator <(Probability probability, Probability probability1)
        {
            return probability.Value < probability1.Value;
        }

        public static bool operator >=(Probability probability, Probability probability1)
        {
            return probability.Value >= probability1.Value;
        }

        public static bool operator <=(Probability probability, Probability probability1)
        {
            return probability.Value <= probability1.Value;
        }

        public bool Equals(Probability other)
        {
            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Probability && Equals((Probability) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}