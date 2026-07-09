using Commons;

namespace PlayerModule
{
    /// <summary>
    ///     装備切り替え中のワールド状態。アクションマップを積んで装備画面を開く。
    /// </summary>
    public class EquipmentState : IState
    {
        public EquipmentState(IInputActionMapStack inputActionMapStack, EquipmentController controller)
        {
            _inputActionMapStack = inputActionMapStack;
            _controller = controller;
        }

        public WorldStateType WorldState => WorldStateType.Equipment;

        public void Entry()
        {
            _inputActionMapStack.Push(InputActionMapType.Equipment);
            _controller.Begin();
        }

        public void Exit()
        {
            _controller.End();
            _inputActionMapStack.Pop();
        }

        private readonly IInputActionMapStack _inputActionMapStack;
        private readonly EquipmentController _controller;
    }
}
