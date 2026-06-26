namespace TypingModule
{
    public class QuestionModel
    {
        public string Question => _question;
        private string _question;
        /// <summary>
        /// 問題を設定する
        /// </summary>
        /// <param name="question"></param>
        public void SetQuestion(string question)
        {
            _question = question;
        }
        /// <summary>
        /// 問題を取得する
        /// </summary>
        /// <returns></returns>
        public string GetQuestion()
        {
            return _question;
        }
    }
}