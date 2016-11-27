namespace Bayes.Data
{
    public class Analysis
    {
        public string Sentence { get; set; }
        public Score[] Words { get; set; }
        public SentenceScore[] Sentences { get; set; }
        public short Score { get; set; }
    }
}