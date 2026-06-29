using System;

namespace Commons
{
    public interface IState
    {
        WorldStateType WorldState { get; }
        void Entry();
        void Exit();
    }
}
