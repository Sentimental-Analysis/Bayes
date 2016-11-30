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
    }
}