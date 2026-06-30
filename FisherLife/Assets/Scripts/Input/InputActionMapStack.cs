using System.Collections.Generic;
using Commons;
using UnityEngine.InputSystem;

namespace InputModule
{
    /// <summary>
    ///     アクションマップの状態を一元管理する疑似的なスタッククラス。
    /// </summary>
    public class InputActionMapStack : IInputActionMapStack
    {
        /// <summary>
        ///     管理対象のアクションアセットを受け取り初期化する。
        /// </summary>
        public InputActionMapStack(InputActionAsset inputActions)
        {
            _assets = inputActions;
            _stack = new Stack<InputActionMapType>();
        }

        /// <summary> 現在有効になっているアクションマップ。 </summary>
        public InputActionMap CurrentMap => _stack.Count > 0 ? MapOf(_stack.Peek()) : null;

        /// <summary>
        ///     現在のマップを無効化し、1つ前のマップを有効化する。
        /// </summary>
        public void Pop()
        {
            if (_stack.Count <= 0)
            {
                return;
            }

            CurrentMap.Disable();
            _stack.Pop();
            CurrentMap.Enable();
        }

        /// <summary>
        ///     現在のマップを無効化し、追加したマップを有効化する。
        /// </summary>
        public void Push(InputActionMapType mapType)
        {
            var nextMap = MapOf(mapType);

            // 既存のマップがあれば無効化する。
            if (_stack.Count > 0)
            {
                CurrentMap.Disable();
            }

            _stack.Push(mapType);
            nextMap.Enable();
        }

        private readonly InputActionAsset _assets;
        private readonly Stack<InputActionMapType> _stack;

        /// <summary>
        ///     アクションマップを探す。見つからなければ例外を送出する。
        /// </summary>
        private InputActionMap MapOf(InputActionMapType mapType)
        {
            return _assets.FindActionMap(mapType.ToString(), throwIfNotFound: true);
        }
    }
}
