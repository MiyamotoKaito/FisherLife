using System;
using Commons;

namespace FishingModule
{
    /// <summary>
    ///     釣り中のワールド状態。
    /// </summary>
    [System.Serializable]
    public class FishingState : IState
    {
        /// <summary> 状態へ入ったときに通知する。 </summary>
        public event Action OnStateChange;

        /// <summary> 対応するワールド状態種別。 </summary>
        public WorldStateType WorldState => WorldStateType.Fishing;

        /// <summary>
        ///     依存を初期化する。
        /// </summary>
        public void Init(IInputActionMapStack inputActionMapStack)
        {
            _inputActionMapStack = inputActionMapStack;
        }

        /// <summary>
        ///     状態に入り、アクションマップを積んで通知する。
        /// </summary>
        public void Entry()
        {
            _inputActionMapStack.Push(InputActionMapType.Player);
            OnStateChange?.Invoke();
        }

        /// <summary>
        ///     状態から出て、アクションマップを戻す。
        /// </summary>
        public void Exit()
        {
            _inputActionMapStack?.Pop();
        }

        private IInputActionMapStack _inputActionMapStack;
    }
}
