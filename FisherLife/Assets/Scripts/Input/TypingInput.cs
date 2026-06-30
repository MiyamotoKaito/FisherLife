using System;
using R3;
using UnityEngine.InputSystem;

namespace InputModule
{
    /// <summary>
    ///     タイピング専用の入力クラス。通常のInputSystemとは扱い方が異なるため独自に実装する。
    /// </summary>
    public class TypingInput : ITypingInput
    {
        /// <summary>
        ///     キーボードのテキスト入力を購読して初期化する。
        /// </summary>
        public TypingInput()
        {
            Keyboard.current.onTextInput += TextInputHandler;
            _onChar = new Subject<char>();
        }

        /// <summary> 入力された1文字を通知する。 </summary>
        public Observable<char> OnChar => _onChar;

        /// <summary>
        ///     入力受付の有効・無効を切り替える。
        /// </summary>
        public void SetEnable(bool enable)
        {
            _enabled = enable;
        }

        /// <summary>
        ///     購読を解除して破棄する。
        /// </summary>
        public void Dispose()
        {
            _onChar?.Dispose();
            _onChar = null;
            if (Keyboard.current != null)
            {
                Keyboard.current.onTextInput -= TextInputHandler;
            }
        }

        private Subject<char> _onChar;
        private bool _enabled;

        /// <summary>
        ///     テキスト入力を受け取り、有効な文字だけを通知する。
        /// </summary>
        private void TextInputHandler(char c)
        {
            // 入力受付が無効なら早期リターンする。
            if (!_enabled)
            {
                return;
            }

            // 制御文字（\b \n \r など）は弾く。
            if (char.IsControl(c))
            {
                return;
            }

            _onChar.OnNext(c);
        }
    }
}
