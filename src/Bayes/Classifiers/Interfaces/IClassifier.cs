namespace Bayes.Classifiers.Interfaces
{
    public interface IClassifier<in TIn, out TOut>
    {
        TOut Classify(TIn source);
    }
}