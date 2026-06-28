using System;

namespace Common
{
    public interface IState
    {
        event Action OnStateChange;
        WorldStateType WorldState { get; }
        void Init(IInputActionMapStack inputActionMapStack);
        void Entry();
        void Exit();
    }
}
