using System;
using R3;

namespace Commons
{
    /// <summary>
    ///     タイピング入力のインターフェース。
    /// </summary>
    public interface ITypingInput : IDisposable
    {
        /// <summary> 入力された1文字を通知する。 </summary>
        Observable<char> OnChar { get; }

        /// <summary>
        ///     入力受付の有効・無効を切り替える。
        /// </summary>
        void SetEnable(bool enable);
    }
}
