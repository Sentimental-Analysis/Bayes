using Bayes.Data;

namespace Bayes.Classifier.Interface
{
    public interface IClassifier<in TIn, out TOut>
    {
        TOut Classify(TIn source);
    }
}