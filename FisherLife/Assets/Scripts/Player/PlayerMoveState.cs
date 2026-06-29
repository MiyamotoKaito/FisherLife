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
        private readonly IInputActionMapStack _inputMapStack;
        private readonly PlayerController _playerController;
        public void Entry()
        {
            _playerController.Enable();
            _inputMapStack.Push(InputActionMapType.Player);
        }

        public void Exit()
        {
            _playerController.Disable();
            _inputMapStack.Pop();
        }
    }
}
