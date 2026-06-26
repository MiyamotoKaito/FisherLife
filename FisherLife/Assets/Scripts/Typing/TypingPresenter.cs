namespace TypingModule
{
    /// <summary>
    /// タイピングの入力を反映させるプレゼンター
    /// </summary>
    public class TypingPresenter
    {
        public TypingPresenter()
        {
            _answerModel = new AnswerModel();
            _questionModel = new QuestionModel();
        }
        private readonly AnswerModel _answerModel;
        private readonly QuestionModel _questionModel;
    }
}