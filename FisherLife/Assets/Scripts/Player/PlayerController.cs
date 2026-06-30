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
            IPlayerMoveUsecase playerMoveUsecase)
        {
            _inputActions = inputActions;
            _playerMovePresenter = playerMovePresenter;
            _playerMoveUsecase = playerMoveUsecase;

            // 同名プロパティ（InputActionMapType.Player）の文字列でマップを取得する。
            _playerMap = _inputActions.FindActionMap(InputActionMapType.ToString(), true);
            _moveAction = _playerMap.FindAction(MOVE_ACTION);
        }

        /// <summary> 対応するアクションマップ種別。 </summary>
        public InputActionMapType InputActionMapType => InputActionMapType.Player;

        /// <summary>
        ///     移動入力の購読を開始する。
        /// </summary>
        public void Enable()
        {
            _moveAction.performed += MoveHandler;
            _moveAction.canceled += MoveHandler;
        }

        /// <summary>
        ///     移動入力の購読を解除する。
        /// </summary>
        public void Disable()
        {
            _moveAction.performed -= MoveHandler;
            _moveAction.canceled -= MoveHandler;
        }

        /// <summary>
        ///     破棄時に購読を解除する。
        /// </summary>
        public void Dispose()
        {
            Disable();
        }

        private const string MOVE_ACTION = "Move";

        private readonly InputActionAsset _inputActions;
        private readonly InputActionMap _playerMap;
        private readonly InputAction _moveAction;
        private readonly PlayerMovePresenter _playerMovePresenter;
        private readonly IPlayerMoveUsecase _playerMoveUsecase;

        /// <summary>
        ///     移動入力を受け取り、変換した方向をPresenterへ渡す。
        /// </summary>
        private void MoveHandler(InputAction.CallbackContext callbackContext)
        {
            var readValue = callbackContext.ReadValue<Vector2>();
            var dir = _playerMoveUsecase.ToVector3(readValue);
            _playerMovePresenter.SetMove(dir);
        }
    }
}
