namespace Bayes.Learner.Interfaces
{
    public interface ILearner<out TResult, in TLearn>
    {
        TResult Learn(TLearn source);
    }
}