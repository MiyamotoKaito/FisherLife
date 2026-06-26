using R3;
namespace TypingModule
{
    /// <summary>
    /// プレイヤーの解答のモデル
    /// </summary>
    public class AnswerModel
    {
        public ReadOnlyReactiveProperty<string> Answer => _answer;
        private ReactiveProperty<string> _answer;
        /// <summary>
        /// 解答欄の文字に追加
        /// </summary>
        /// <param name="c"></param>
        public void Write(char c)
        {
            _answer.Value += c;
        }
        /// <summary>
        /// 解答欄の文字一つを消す
        /// </summary>
        public void Delete()
        {

        }
    }
}