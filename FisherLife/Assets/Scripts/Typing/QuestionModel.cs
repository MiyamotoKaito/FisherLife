using System;
using R3;

namespace TypingModule
{
    /// <summary>
    /// 問題のモデル。出題文字列と「今どこまで正しく打てたか」を管理する。
    /// </summary>
    public class QuestionModel : IDisposable
    {
        public ReadOnlyReactiveProperty<string> Question => _question;
        /// <summary>現在の問題を最後まで打ち切ったか</summary>
        public bool IsCompleted => !string.IsNullOrEmpty(_question.Value) && _index >= _question.Value.Length;

        private readonly ReactiveProperty<string> _question = new(string.Empty);
        private int _index;

        /// <summary>
        /// 問題を設定する（進捗はリセット）
        /// </summary>
        public void SetQuestion(string question)
        {
            _question.Value = question;
            _index = 0;
        }

        /// <summary>
        /// 入力された1文字を現在位置と照合する。正しければ true。
        /// </summary>
        public bool Input(char c)
        {
            var q = _question.Value;
            if (string.IsNullOrEmpty(q)) return false;
            if (_index >= q.Length) return false;
            if (q[_index] != c) return false;   // ミスタイプは無視

            _index++;
            return true;
        }

        public void Dispose()
        {
            _question.Dispose();
        }
    }
}
