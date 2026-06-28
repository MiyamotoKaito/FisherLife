using Commons;
using R3;
using UnityEngine.InputSystem;

namespace InputModule
{
    /// <summary>
    ///     タイピング専用のInputクラス。通常のInputSystemの運用方法とは勝手が違うため
    /// </summary>
    public class TypingInput : ITypingInput
    {
        public TypingInput()
        {
            Keyboard.current.onTextInput += OnTextInput;
            _onChar = new();
        }
        /// <summary>入力された値を知らせる</summary>
        public Observable<char> OnChar => _onChar;
        public void SetEnable(bool enable) => _enabled = enable;

        private Subject<char> _onChar;

        private bool _enabled;
        public void Dispose()
        {
            _onChar?.Dispose();
            _onChar = null;
            if (Keyboard.current != null)
            {
                Keyboard.current.onTextInput -= OnTextInput;
            }
        }
        private void OnTextInput(char c)
        {
            if (!_enabled) return;//有効ではなかったら早期リターン

            if (char.IsControl(c)) return; // \b \n \r などを弾く

            _onChar.OnNext(c);
        }
    }
}
