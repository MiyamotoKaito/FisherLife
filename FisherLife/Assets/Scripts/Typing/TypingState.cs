using Common;
using UnityEngine;

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
        }

        public void Exit()
        {
            _inputActionMapStack.Pop();
        }

        private IInputActionMapStack _inputActionMapStack;
    }
}
