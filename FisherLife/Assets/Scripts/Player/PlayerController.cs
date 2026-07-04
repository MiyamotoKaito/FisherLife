using Commons;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerModule
{
    /// <summary>
    ///     プレイヤーの入力を受け取り移動へ反映させるコントローラー。
    /// </summary>
    public class PlayerController : IPlayerController
    {
        /// <summary>
        ///     依存を受け取り、移動用の入力アクションを取得する。
        /// </summary>
        public PlayerController(InputActionAsset inputActions,
            PlayerMovePresenter playerMovePresenter,
            IPlayerMoveUsecase playerMoveUsecase,
            IWorldStateMachine worldStateMachine)
        {
            _inputActions = inputActions;
            _playerMovePresenter = playerMovePresenter;
            _playerMoveUsecase = playerMoveUsecase;
            _worldStateMachine = worldStateMachine;

            // 同名プロパティ（InputActionMapType.Player）の文字列でマップを取得する。
            _playerMap = _inputActions.FindActionMap(InputActionMapType.ToString(), true);
            _moveAction = _playerMap.FindAction(MOVE_ACTION);
            _interactAction = _playerMap.FindAction(INTERACT_ACTION);
        }

        /// <summary> 対応するアクションマップ種別。 </summary>
        public InputActionMapType InputActionMapType => InputActionMapType.Player;

        /// <summary>
        ///     移動入力の購読を開始する。
        /// </summary>
        public void Begin()
        {
            _moveAction.performed += MoveHandler;
            _moveAction.canceled += MoveHandler;
            _interactAction.started += InteractHandler;
        }

        /// <summary>
        ///     移動入力の購読を解除する。
        /// </summary>
        public void End()
        {
            _moveAction.performed -= MoveHandler;
            _moveAction.canceled -= MoveHandler;
            _interactAction.started -= InteractHandler;
        }

        /// <summary>
        ///     破棄時に購読を解除する。
        /// </summary>
        public void Dispose()
        {
            End();
        }

        private const string MOVE_ACTION = "Move";
        private const string INTERACT_ACTION = "Interact";

        private readonly InputActionAsset _inputActions;
        private readonly InputActionMap _playerMap;
        private readonly InputAction _moveAction;
        private readonly InputAction _interactAction;
        private readonly PlayerMovePresenter _playerMovePresenter;
        private readonly IPlayerMoveUsecase _playerMoveUsecase;
        private readonly IWorldStateMachine _worldStateMachine;

        /// <summary>
        ///     移動入力を受け取り、変換した方向をPresenterへ渡す。
        /// </summary>
        private void MoveHandler(InputAction.CallbackContext callbackContext)
        {
            var readValue = callbackContext.ReadValue<Vector2>();
            var dir = _playerMoveUsecase.ToVector3(readValue);
            _playerMovePresenter.SetMove(dir);
        }

        private void InteractHandler(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.started)
            {
                _worldStateMachine.ChangeState(WorldStateType.Fishing);
            }
        }
    }
}