using System.Collections.Generic;

namespace Bayes.Data
{
    public class Classification
    {
        public Probability Probability { get; }
        public Category Category { get; }
        public IEnumerable<string> Words { get; }

        public Classification(Probability probability, Category category, IEnumerable<string> words)
        {
            Probability = probability;
            Category = category;
            Words = words;
        }

        public Classification(Category category, IEnumerable<string> words)
        {
            Probability = 1.0;
            Category = category;
            Words = words;
        }
    }
}