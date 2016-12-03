namespace Bayes.Data
{
    public class Sentence
    {
        public Sentence(string text, WordCategory category)
        {
            Text = text;
            Category = category;
        }

        public string Text { get; }
        public WordCategory Category { get; }

        protected bool Equals(Sentence other)
        {
            return string.Equals(Text, other.Text) && Category == other.Category;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Sentence) obj);
        }

        public static bool operator ==(Sentence sentence, Sentence sentence1)
        {
            return sentence != null && sentence.Equals(sentence1);
        }

        public static bool operator !=(Sentence sentence, Sentence sentence1)
        {
            return !(sentence == sentence1);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Text?.GetHashCode() ?? 0) * 397) ^ (int) Category;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Text)}: {Text}, {nameof(Category)}: {Category}";
        }
    }
}