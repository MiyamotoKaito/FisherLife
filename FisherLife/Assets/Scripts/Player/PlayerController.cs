using Commons;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerModule
{
    public class PlayerController : IPlayerController
    {
        public PlayerController(InputActionAsset inputActions,
            PlayerMovePresenter playerMovePresenter,
            IPlayerMoveUsecase playerMoveUsecase)
        {
            _inputActions = inputActions;
            _playerMovePresenter = playerMovePresenter;
            _playerMoveUsecase = playerMoveUsecase;

            _playerMap = _inputActions.FindActionMap(InputActionMapType.ToString(), true);
            _moveAction = _playerMap.FindAction("Move");
        }
        public InputActionMapType InputActionMapType => InputActionMapType.Player;
        private readonly InputActionAsset _inputActions;
        private readonly InputActionMap _playerMap;
        private readonly InputAction _moveAction;
        private readonly PlayerMovePresenter _playerMovePresenter;
        private readonly IPlayerMoveUsecase _playerMoveUsecase;
        public void Enable()
        {
            _moveAction.performed += OnMove;
            _moveAction.canceled += OnMove;
        }

        public void Disable()
        {
            _moveAction.performed -= OnMove;
            _moveAction.canceled -= OnMove;
        }

        public void Dispose()
        {
            Disable();
        }
        private void OnMove(InputAction.CallbackContext callbackContext)
        {
            var readValue = callbackContext.ReadValue<Vector2>();
            var dir = _playerMoveUsecase.ToVector3(readValue);
            _playerMovePresenter.SetMove(dir);
        }
    }
}
