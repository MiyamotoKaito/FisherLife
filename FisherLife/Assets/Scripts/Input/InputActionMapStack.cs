using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace InputModule
{
    /// <summary>
    /// アクションマップの状態を一元管理する疑似的なスタッククラス
    /// </summary>
    public class InputActionMapStack
    {
        public InputActionMapStack(InputActionAsset inputActions)
        {
            _assets = inputActions;
        }
        /// <summary>現在有効になっているアクションマップ</summary>
        public InputActionMap CurrentMap => _stack.Count > 0 ? MapOf(_stack.Peek()): null;
        /// <summary>
        /// 現在のアクションマップを無効化して一つ前のアクションマップを有効化する
        /// </summary>
        public void Pop()
        {
            if (_stack.Count <= 0) return;

            CurrentMap.Disable();
            _stack.Pop();
            CurrentMap.Enable();
        }
        /// <summary>
        /// 現在のアクションマップを無効化して追加されたアクションマップを有効化する
        /// </summary>
        /// <param name="mapType"></param>
        public void Push(InputActionMapType mapType)
        {
            var nextMap = MapOf(mapType);
            CurrentMap.Disable();
            _stack.Push(mapType);
            nextMap.Enable();
        }
        /// <summary>
        /// アクションマップを探す
        /// 無かったら例外が吐かれる
        /// </summary>
        /// <param name="mapType"></param>
        /// <returns></returns>
        private InputActionMap MapOf(InputActionMapType mapType)
        {
            return _assets.FindActionMap(mapType.ToString(), throwIfNotFound: true);
        }
        private readonly InputActionAsset _assets;
        private Stack<InputActionMapType> _stack;
    }
}
