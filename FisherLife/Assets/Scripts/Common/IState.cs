using System;

namespace Common
{
    public interface IState
    {
        event Action OnStateChenge;
        WorldStateType WorldState { get; }
        void Init(IInputActionMapStack inputActionMapStack);
        void Entry();
        void Exit();
    }
}
