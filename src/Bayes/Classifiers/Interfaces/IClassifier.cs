namespace Bayes.Classifiers.Interfaces
{
    public interface IClassifier<out TScore, in TParameter, in TState>
    {
        TScore Classify(TParameter parameter, TState state);
    }
}