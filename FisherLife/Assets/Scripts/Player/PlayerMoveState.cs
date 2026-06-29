using Commons;
using System;

namespace PlayerModule
{
    public class PlayerMoveState : IState
    {
        public PlayerMoveState(IInputActionMapStack inputActionMapStack, PlayerController playerController)
        {
            _inputMapStack = inputActionMapStack;
            _playerController = playerController;
        }
        public WorldStateType WorldState => WorldStateType.Moving;

        public event Action OnStateChange;
        private readonly IInputActionMapStack _inputMapStack;
        private readonly PlayerController _playerController;
        public void Entry()
        {
            _inputMapStack.Push(InputActionMapType.Player);
        }

        public void Exit()
        {
            _inputMapStack.Pop();
        }
    }
}
