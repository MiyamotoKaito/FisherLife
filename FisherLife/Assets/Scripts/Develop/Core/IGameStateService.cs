using System;

namespace Miyamoto.FisherLife.Develop.Core
{
    public interface IGameStateService
    {
        GameState Current { get; }
        void Transition(GameState next);
        IObservable<GameState> OnStateChange { get; }
    }
}