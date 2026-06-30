using Commons;

namespace PlayerModule
{
    /// <summary>
    ///     プレイヤー移動中のワールド状態。
    /// </summary>
    public class PlayerMoveState : IState
    {
        /// <summary>
        ///     依存を受け取り初期化する。
        /// </summary>
        public PlayerMoveState(IInputActionMapStack inputActionMapStack, PlayerController playerController)
        {
            _inputMapStack = inputActionMapStack;
            _playerController = playerController;
        }

        /// <summary> 対応するワールド状態種別。 </summary>
        public WorldStateType WorldState => WorldStateType.Moving;

        /// <summary>
        ///     状態に入り、入力を有効化してアクションマップを積む。
        /// </summary>
        public void Entry()
        {
            _playerController.Begin();
            _inputMapStack.Push(InputActionMapType.Player);
        }

        /// <summary>
        ///     状態から出て、入力を無効化してアクションマップを戻す。
        /// </summary>
        public void Exit()
        {
            _playerController.End();
            _inputMapStack.Pop();
        }

        private readonly IInputActionMapStack _inputMapStack;
        private readonly PlayerController _playerController;
    }
}
