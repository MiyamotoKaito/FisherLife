using Commons;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility;

namespace ShopModule
{
    public class ShoppingController : IController
    {
        public ShoppingController(
            InputActionAsset inputActions,
            IWorldStateMachine worldStateMachine,
            SelectTradePanel rootPanel,
            ComfilmPanel comfilmPanel)
        {
            _inputActions = inputActions;
            _worldStateMachine = worldStateMachine;
            _rootPanel = rootPanel;
            _comfilmPanel = comfilmPanel;

            _shoppingActionMap = _inputActions.FindActionMap("Shopping");
            _upAction = _shoppingActionMap.FindAction("Up");
            _downAction = _shoppingActionMap.FindAction("Down");
            _rightAction = _shoppingActionMap.FindAction("Right");
            _leftAction = _shoppingActionMap.FindAction("Left");
            _entryAction = _shoppingActionMap.FindAction("Entry");
            _cancelAction = _shoppingActionMap.FindAction("Cancel"); // 未定義なら null
        }

        public InputActionMapType InputActionMapType => InputActionMapType.Shopping;

        private readonly Stack<ShoppingPanelBase> _shoppingPanelStack = new();
        private readonly InputActionAsset _inputActions;
        private readonly IWorldStateMachine _worldStateMachine;
        private readonly SelectTradePanel _rootPanel;
        private readonly ComfilmPanel _comfilmPanel;
        private readonly InputActionMap _shoppingActionMap;
        private readonly InputAction _upAction;
        private readonly InputAction _downAction;
        private readonly InputAction _rightAction;
        private readonly InputAction _leftAction;
        private readonly InputAction _entryAction;
        private readonly InputAction _cancelAction;

        public void Begin()
        {
            _upAction.started += Up;
            _downAction.started += Down;
            _rightAction.started += Right;
            _leftAction.started += Left;
            _entryAction.started += Entry;
            if (_cancelAction != null) _cancelAction.started += Cancel;

            Push(_rootPanel);
        }

        public void End()
        {
            _upAction.started -= Up;
            _downAction.started -= Down;
            _rightAction.started -= Right;
            _leftAction.started -= Left;
            _entryAction.started -= Entry;
            if (_cancelAction != null) _cancelAction.started -= Cancel;

            Clear();
        }

        public void Dispose() => End();

        public ShoppingPanelBase Peak() => _shoppingPanelStack.Peek();

        public void Push(ShoppingPanelBase panel)
        {
            // 今表示中のパネルを隠す（上書き表示にしない）。
            if (_shoppingPanelStack.Count > 0)
            {
                _shoppingPanelStack.Peek().gameObject.SetActive(false);
            }

            panel.gameObject.SetActive(true);
            _shoppingPanelStack.Push(panel);
            panel.Begin();
        }

        ///<summary>
        ///取引対象をセットして確認パネルを積む。
        ///</summary>
        public void PushConfirm(TradeItem item)
        {
            _comfilmPanel.Setup(item);
            Push(_comfilmPanel);
        }

        public void Pop()
        {
            if (_shoppingPanelStack.Count == 0) return;

            var closed = _shoppingPanelStack.Pop();
            closed.gameObject.SetActive(false);

            // 全部閉じたら店を出る。
            if (_shoppingPanelStack.Count == 0)
            {
                _worldStateMachine.BackState();
                return;
            }

            var prev = _shoppingPanelStack.Peek();
            prev.gameObject.SetActive(true);
            prev.Begin();
        }

        // 退店時の後始末として全パネルを閉じてスタックを空にする。
        private void Clear()
        {
            while (_shoppingPanelStack.Count > 0)
            {
                // Play停止/シーン破棄ではパネルが先に破棄されていることがあるためガードする。
                var panel = _shoppingPanelStack.Pop();
                if (panel != null) panel.gameObject.SetActive(false);
            }
        }

        private void Up(InputAction.CallbackContext context) => Peak().Up();
        private void Down(InputAction.CallbackContext context) => Peak().Down();
        private void Right(InputAction.CallbackContext context) => Peak().Right();
        private void Left(InputAction.CallbackContext context) => Peak().Left();
        private void Entry(InputAction.CallbackContext context) => Peak().Entry(this);
        private void Cancel(InputAction.CallbackContext context) => Pop();
    }
}
