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
            Camera camera,
            PlayerInteractor playerInteractor,
            IPlayerMoveUsecase playerMoveUsecase,
            IWorldStateMachine worldStateMachine)
        {
            _inputActions = inputActions;
            _playerMovePresenter = playerMovePresenter;
            _camera = camera;
            _playerMoveUsecase = playerMoveUsecase;
            _playerInteractor = playerInteractor;
            _worldStateMachine = worldStateMachine;

            // 同名プロパティ（InputActionMapType.Player）の文字列でマップを取得する。
            _playerMap = _inputActions.FindActionMap(InputActionMapType.ToString(), true);
            _moveAction = _playerMap.FindAction(MOVE_ACTION);
            _interactAction = _playerMap.FindAction(INTERACT_ACTION);
            _equipmentAction = _playerMap.FindAction(EQUIPMENT_ACTION); // 未定義なら null
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
            if (_equipmentAction != null) _equipmentAction.started += OpenEquipmentHandler;
        }

        /// <summary>
        ///     移動入力の購読を解除する。
        /// </summary>
        public void End()
        {
            _moveAction.performed -= MoveHandler;
            _moveAction.canceled -= MoveHandler;
            _interactAction.started -= InteractHandler;
            if (_equipmentAction != null) _equipmentAction.started -= OpenEquipmentHandler;
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
        private const string EQUIPMENT_ACTION = "Equipment";

        private readonly InputActionAsset _inputActions;
        private readonly InputActionMap _playerMap;
        private readonly InputAction _moveAction;
        private readonly InputAction _interactAction;
        private readonly InputAction _equipmentAction;
        private readonly PlayerMovePresenter _playerMovePresenter;
        private readonly IPlayerMoveUsecase _playerMoveUsecase;
        private readonly Camera _camera;
        private readonly PlayerInteractor _playerInteractor;
        private readonly IWorldStateMachine _worldStateMachine;

        /// <summary>
        ///     移動入力を受け取り、変換した方向をPresenterへ渡す。
        /// </summary>
        private void MoveHandler(InputAction.CallbackContext callbackContext)
        {
            var readValue = callbackContext.ReadValue<Vector2>();
            var dir = _playerMoveUsecase.ToVector3(readValue);
            // カメラの向きに合わせて移動方向を変換する
            var calculatedDir = _camera.transform.TransformDirection(dir);
            _playerMovePresenter.SetMove(calculatedDir);
        }

        private void InteractHandler(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.started)
            {
                _playerInteractor.Interact();
            }
        }

        private void OpenEquipmentHandler(InputAction.CallbackContext callbackContext)
        {
            _worldStateMachine.ChangeState(WorldStateType.Equipment);
        }
    }
}
