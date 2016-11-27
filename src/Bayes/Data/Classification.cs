using System.Collections.Generic;

namespace Bayes.Data
{
    public class Classification
    {
        public Probability Probability { get; set; }
        public Category Category { get; set; }
        public IEnumerable<string> Words { get; set; }
    }
}