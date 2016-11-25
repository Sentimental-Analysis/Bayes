namespace Bayes.Predicators.Interfaces
{
    public interface IPredicator<in TIn, out TOut>
    {
        TOut Predict(TIn source);
    }
}