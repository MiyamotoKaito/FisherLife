using System;

namespace Commons
{
    public interface IState
    {
        event Action OnStateChange;
        WorldStateType WorldState { get; }
        void Entry();
        void Exit();
    }
}
