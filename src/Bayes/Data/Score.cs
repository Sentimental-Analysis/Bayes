namespace Bayes.Data
{
    public class Score
    {
        public Sentence Sentence { get; }

        public Probability Probability { get; }

        public Score(Sentence sentence, Probability probability)
        {
            Sentence = sentence;
            Probability = probability;
        }
    }
}