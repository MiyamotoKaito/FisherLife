using Commons;

namespace ShopModule
{
    /// <summary>
    ///     買い物中のワールド状態。アクションマップを積んでショップ操作を開始する。
    /// </summary>
    public class ShoppingState : IState
    {
        public ShoppingState(IInputActionMapStack inputActionMapStack, ShoppingController shoppingController)
        {
            _inputActionMapStack = inputActionMapStack;
            _shoppingController = shoppingController;
        }

        public WorldStateType WorldState => WorldStateType.Shopping;

        public void Entry()
        {
            _inputActionMapStack.Push(InputActionMapType.Shopping);
            _shoppingController.Begin();
        }

        public void Exit()
        {
            _shoppingController.End();
            _inputActionMapStack.Pop();
        }

        private readonly IInputActionMapStack _inputActionMapStack;
        private readonly ShoppingController _shoppingController;
    }
}
