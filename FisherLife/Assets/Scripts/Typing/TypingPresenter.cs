using System;
using System.Threading;
using Commons;
using R3;

namespace TypingModule
{
    /// <summary>
    ///     タイピングの入力を解答・表示へ反映させるPresenter。
    /// </summary>
    public class TypingPresenter : IDisposable
    {
        /// <summary>
        ///     依存とモデルを準備し、購読を開始する。
        /// </summary>
        public TypingPresenter(ITypingInput typingInput, ITypingView typingView)
        {
            _typingInput = typingInput;
            _typingView = typingView;
            _answerModel = new AnswerModel();
            _questionModel = new QuestionModel();
            _tokenSource = new CancellationTokenSource();

            Subscribe();
        }

        /// <summary> 1問を打ち切ったときの通知。次の問題出題などに使う。 </summary>
        public event Action Completed;

        /// <summary>
        ///     問題を設定して入力受付を開始する。
        /// </summary>
        public void StartTyping(string question)
        {
            _questionModel.SetQuestion(question);
            _answerModel.Clear();
            _typingInput.SetEnable(true);
        }

        /// <summary>
        ///     入力受付を止める。
        /// </summary>
        public void StopTyping()
        {
            _typingInput.SetEnable(false);
        }

        /// <summary>
        ///     購読を解除し、モデルを破棄する。
        /// </summary>
        public void Dispose()
        {
            _tokenSource?.Cancel();
            _answerModel?.Dispose();
            _questionModel?.Dispose();
        }

        private readonly AnswerModel _answerModel;
        private readonly QuestionModel _questionModel;
        private readonly ITypingInput _typingInput;
        private readonly ITypingView _typingView;
        private readonly CancellationTokenSource _tokenSource;

        /// <summary>
        ///     入力と表示の購読を登録する。
        /// </summary>
        private void Subscribe()
        {
            // 入力1文字を照合し、正しく打てた文字だけ解答欄に積む。
            _typingInput.OnChar.Subscribe(c =>
            {
                // ミスタイプは無視する。
                if (!_questionModel.Input(c))
                {
                    return;
                }

                // 解答欄へ反映する。
                _answerModel.Write(c);

                // 完了したら通知する。
                if (_questionModel.IsCompleted)
                {
                    Completed?.Invoke();
                }
            }).RegisterTo(_tokenSource.Token);

            // モデルの変更をViewへ反映する。
            _answerModel.Answer.Subscribe(_typingView.UpdateAnswer).RegisterTo(_tokenSource.Token);
            _questionModel.Question.Subscribe(_typingView.SetQuestion).RegisterTo(_tokenSource.Token);
        }
    }
}
