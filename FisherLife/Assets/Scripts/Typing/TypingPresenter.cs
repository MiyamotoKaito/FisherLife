using Common;
using R3;
using System;
using System.Linq;
using System.Threading;

namespace TypingModule
{
    /// <summary>
    /// タイピングの入力を反映させるプレゼンター
    /// </summary>
    public class TypingPresenter : IDisposable
    {
        public TypingPresenter(ITypingInput typingInput, ITypingView typingView)
        {
            _typingInput = typingInput;
            _typingView = typingView;
            _answerModel = new AnswerModel();
            _questionModel = new QuestionModel();
            _tokenSource = new();

            Subscribe();
        }
        /// <summary>
        /// 文字の入力表示を登録するメソッド
        /// </summary>
        private void Subscribe()
        {
            _typingInput.OnChar.Subscribe(c =>
            {
                _answerModel.Write(c);
            }).RegisterTo(_tokenSource.Token);

            _answerModel.Answer.Subscribe(str =>
            {
                _questionModel.AddChar(str.First());
                _typingView.UpdateAnswer(str);
            }).RegisterTo(_tokenSource.Token);

            _questionModel.Question.Subscribe(_typingView.SetQuestion).RegisterTo(_tokenSource.Token);
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _typingInput?.Dispose();
        }

        private readonly AnswerModel _answerModel;
        private readonly QuestionModel _questionModel;
        private readonly ITypingInput _typingInput;
        private readonly ITypingView _typingView;
        private CancellationTokenSource _tokenSource;
    }
}