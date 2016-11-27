namespace Bayes.Teachers.Interfaces
{
    public interface ITeacher<in TIn, out TOut>
    {
        TOut Learn(TIn source);
    }
}