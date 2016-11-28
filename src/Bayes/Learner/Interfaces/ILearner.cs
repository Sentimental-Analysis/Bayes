namespace Bayes.Learner.Interfaces
{
    public interface ILearner<TState, in TLearn>
    {
        TState Learn(TState oldState, TLearn source);
    }
}