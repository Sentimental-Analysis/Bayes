using Bayes.Classifiers.Interfaces;
using Bayes.Data;

namespace Bayes.Classifiers.Implementations
{
    public abstract class Classifier<TIn, TOut> : IClassifier<TIn, TOut>
    {
        public TOut Classify(TIn source)
        {
            throw new System.NotImplementedException();
        }

        public void Learn(Category category, TIn source)
        {
            throw new System.NotImplementedException();
        }
    }
}