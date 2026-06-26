using R3;

namespace TypingModule
{
    /// <summary>
    /// 問題のモデル
    /// </summary>
    public class QuestionModel
    {
        public ReadOnlyReactiveProperty<string> Question => _question;
        private ReactiveProperty<string> _question;
        private ReactiveProperty<string> _currentQuestion;
        private int _index = 0;
        /// <summary>
        /// 問題を設定する
        /// </summary>
        /// <param name="question"></param>
        public void SetQuestion(string question)
        {
            _question.Value = question;
            _index = question.Length;
            _currentQuestion.Value = string.Empty;
        }
        public void AddChar(char c)
        {
            if (_question.Value[_index] != c)
                return;

            _currentQuestion.Value += c;
            return;
        }
    }
}