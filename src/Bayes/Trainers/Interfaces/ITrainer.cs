using System.Collections.Generic;

namespace Bayes.Trainers.Interfaces
{
    public interface ITrainer<out TState, in TTrain>
    {
        void Train(TTrain trainData);
        void Train(IEnumerable<TTrain> trainDataArray);
        TState CurrentState { get; }
    }
}