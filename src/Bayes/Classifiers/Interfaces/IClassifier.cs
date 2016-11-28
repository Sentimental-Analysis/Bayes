namespace Bayes.Classifiers.Interfaces
{
    public interface IClassifier<out TScore, in TParameter>
    {
        TScore Classify(TParameter parameter);
    }
}