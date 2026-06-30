using UnityEngine.InputSystem;

namespace Commons
{
    /// <summary>
    ///     アクションマップをスタックで一元管理するインターフェース。
    /// </summary>
    public interface IInputActionMapStack
    {
        /// <summary> 現在有効になっているアクションマップ。 </summary>
        InputActionMap CurrentMap { get; }

        /// <summary>
        ///     現在のマップを無効化し、1つ前のマップを有効化する。
        /// </summary>
        void Pop();

        /// <summary>
        ///     現在のマップを無効化し、指定したマップを有効化する。
        /// </summary>
        void Push(InputActionMapType mapType);
    }
}
