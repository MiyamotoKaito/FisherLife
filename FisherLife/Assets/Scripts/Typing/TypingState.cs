using Commons;
using System;

namespace TypingModule
{
    [System.Serializable]
    public class TypingState : IState
    {
        public WorldStateType WorldState => WorldStateType.Typing;
        public TypingState(IInputActionMapStack inputActionMapStack,
            TypingController typingController)
        {
            _inputActionMapStack = inputActionMapStack;
            _typingController = typingController;
        }

        public void Entry()
        {
            _inputActionMapStack.Push(InputActionMapType.Typing);
            _typingController.Enable();
        }

        public void Exit()
        {
            _inputActionMapStack.Pop();
            _typingController.Disable();
        }

        private readonly IInputActionMapStack _inputActionMapStack;
        private readonly TypingController _typingController;
    }
}
