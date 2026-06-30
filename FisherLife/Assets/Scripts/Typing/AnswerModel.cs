using System;
using R3;

namespace TypingModule
{
    /// <summary>
    ///     プレイヤーの解答欄（正しく打てた文字列）のモデル。
    /// </summary>
    public class AnswerModel : IDisposable
    {
        /// <summary> 現在の解答文字列。 </summary>
        public ReadOnlyReactiveProperty<string> Answer => _answer;

        /// <summary>
        ///     解答欄に1文字追加する。
        /// </summary>
        public void Write(char c)
        {
            _answer.Value += c;
        }

        /// <summary>
        ///     解答欄の末尾を1文字消す。
        /// </summary>
        public void Delete()
        {
            if (_answer.Value.Length > 0)
            {
                _answer.Value = _answer.Value[..^1];
            }
        }

        /// <summary>
        ///     解答欄を空にする。
        /// </summary>
        public void Clear()
        {
            _answer.Value = string.Empty;
        }

        /// <summary>
        ///     リアクティブプロパティを破棄する。
        /// </summary>
        public void Dispose()
        {
            _answer?.Dispose();
        }

        private readonly ReactiveProperty<string> _answer = new(string.Empty);
    }
}
