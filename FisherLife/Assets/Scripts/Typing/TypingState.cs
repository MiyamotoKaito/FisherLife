using Commons;

namespace TypingModule
{
    /// <summary>
    ///     タイピング中のワールド状態。
    /// </summary>
    [System.Serializable]
    public class TypingState : IState
    {
        /// <summary>
        ///     依存を受け取り初期化する。
        /// </summary>
        public TypingState(IInputActionMapStack inputActionMapStack,
            TypingController typingController)
        {
            _inputActionMapStack = inputActionMapStack;
            _typingController = typingController;
        }

        /// <summary> 対応するワールド状態種別。 </summary>
        public WorldStateType WorldState => WorldStateType.Typing;

        /// <summary>
        ///     状態に入り、アクションマップを積んで出題を開始する。
        /// </summary>
        public void Entry()
        {
            _inputActionMapStack.Push(InputActionMapType.Typing);
            _typingController.Enable();
        }

        /// <summary>
        ///     状態から出て、アクションマップを戻して出題を停止する。
        /// </summary>
        public void Exit()
        {
            _inputActionMapStack.Pop();
            _typingController.Disable();
        }

        private readonly IInputActionMapStack _inputActionMapStack;
        private readonly TypingController _typingController;
    }
}
