using Commons;
using System;

namespace TypingModule
{
    [System.Serializable]
    public class TypingState : IState
    {
        public WorldStateType WorldState => WorldStateType.Typing;
        public void Init(IInputActionMapStack inputActionMapStack)
        {
            _inputActionMapStack = inputActionMapStack;
        }

        public void Entry()
        {
            _inputActionMapStack.Push(InputActionMapType.Typing);
            OnStateChange?.Invoke();
        }

        public void Exit()
        {
            _inputActionMapStack.Pop();
        }

        private IInputActionMapStack _inputActionMapStack;

        public event Action OnStateChange;
    }
}
