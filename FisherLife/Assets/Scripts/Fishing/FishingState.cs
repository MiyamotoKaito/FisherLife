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
        /// <summary>
        ///     依存を初期化する。
        /// </summary>
        public FishingState(IInputActionMapStack inputActionMapStack, IFishingController fishingController)
        {
            _inputActionMapStack = inputActionMapStack;
            _fishingController = fishingController;
        }

        /// <summary> 対応するワールド状態種別。 </summary>
        public WorldStateType WorldState => WorldStateType.Fishing;

        /// <summary>
        ///     状態に入り、アクションマップを積んで通知する。
        /// </summary>
        public void Entry()
        {
            _inputActionMapStack.Push(InputActionMapType.Fishing);
            _fishingController.Begin();
        }

        /// <summary>
        ///     状態から出て、アクションマップを戻す。
        /// </summary>
        public void Exit()
        {
            _inputActionMapStack?.Pop();
            _fishingController.End();
        }
        private readonly IFishingController _fishingController;
        private readonly IInputActionMapStack _inputActionMapStack;
    }
}
