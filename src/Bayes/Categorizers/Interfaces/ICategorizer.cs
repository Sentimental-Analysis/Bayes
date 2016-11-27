namespace Bayes.Categorizers.Interfaces
{
    public interface ICategorizer<in TIn, out TOut>
    {
        TOut Categorize(TIn source);
    }
}