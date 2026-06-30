using System;

namespace Commons
{
    /// <summary>
    ///     入力を有効・無効化できるコントローラーのインターフェース。
    /// </summary>
    public interface IController : IDisposable
    {
        /// <summary> 対応するアクションマップ種別。 </summary>
        InputActionMapType InputActionMapType { get; }

        /// <summary>
        ///     入力を有効化する。
        /// </summary>
        void Begin();

        /// <summary>
        ///     入力を無効化する。
        /// </summary>
        void End();
    }
}
