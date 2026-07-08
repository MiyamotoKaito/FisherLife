using Commons;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace ShopModule
{
    public class ShoppingController : IController
    {
        public ShoppingController(InputActionAsset inputActions)
        {
            _inputActions = inputActions;

            _shoppingActionMap = _inputActions.FindActionMap("Shopping");
            _upAction = _shoppingActionMap.FindAction("Up");
            _downAction = _shoppingActionMap.FindAction("Down");
            _rightAction = _shoppingActionMap.FindAction("Right");
            _leftAction = _shoppingActionMap.FindAction("Left");
            _entryAction = _shoppingActionMap.FindAction("Entry");
        }
        public InputActionMapType InputActionMapType => InputActionMapType.Shopping;
        private Stack<ShoppingPanelBase> _shoppingPanelStack = new();
        private readonly InputActionAsset _inputActions;
        private readonly InputActionMap _shoppingActionMap;
        private readonly InputAction _upAction;
        private readonly InputAction _downAction;
        private readonly InputAction _rightAction;
        private readonly InputAction _leftAction;
        private readonly InputAction _entryAction;
        public void Begin()
        {
            _upAction.started += Up;
            _downAction.started += Down;
            _rightAction.started += Right;
            _leftAction.started += Left;
            _entryAction.started += Entry;
        }

        public void Dispose()
        {
            _upAction.started -= Up;
            _downAction.started -= Down;
            _rightAction.started -= Right;
            _leftAction.started -= Left;
            _entryAction.started -= Entry;
        }

        public void End()
        {
            _upAction.started -= Up;
            _downAction.started -= Down;
            _rightAction.started -= Right;
            _leftAction.started -= Left;
            _entryAction.started -= Entry;
        }

        public ShoppingPanelBase Peak()
        {
            return _shoppingPanelStack.Peek();
        }

        public void Pop()
        {
            if (_shoppingPanelStack.Count > 0)
            {
                _shoppingPanelStack.Pop();
                Peak().Begin();
            }
        }

        public void Push(ShoppingPanelBase panel)
        {
            _shoppingPanelStack.Push(panel);
            Peak().Begin();
        }
        private void Up(InputAction.CallbackContext context)
        {
            Peak().Up();
        }
        private void Down(InputAction.CallbackContext context)
        {
            Peak().Down();
        }
        private void Right(InputAction.CallbackContext context)
        {
            Peak().Right();
        }
        private void Left(InputAction.CallbackContext context)
        {
            Peak().Left();
        }
        private void Entry(InputAction.CallbackContext context)
        {
            Peak().Entry();
        }
    }
}
