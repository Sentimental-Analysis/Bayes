
namespace Bayes.Teachers.Interfaces
{
    public interface ITeacher<TIn, TOut>
    {
        TOut Learn(TOut oldResult, TIn source);
    }
}