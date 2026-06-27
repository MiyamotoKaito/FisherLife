using Common;
using R3;
using System;
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

        /// <summary>1問を打ち切ったときの通知（次の問題出題などに使う）</summary>
        public event Action Completed;

        /// <summary>
        /// 入力と表示の購読を登録する
        /// </summary>
        private void Subscribe()
        {
            // 入力1文字を照合し、正しく打てた文字だけ解答欄に積む
            _typingInput.OnChar.Subscribe(c =>
            {
                if (!_questionModel.Input(c)) return;   // ミスタイプは無視

                _answerModel.Write(c);                  // 先に解答欄へ反映してから
                if (_questionModel.IsCompleted)         // 完了判定（順序が重要）
                    Completed?.Invoke();
            }).RegisterTo(_tokenSource.Token);

            // モデル → View
            _answerModel.Answer.Subscribe(_typingView.UpdateAnswer).RegisterTo(_tokenSource.Token);
            _questionModel.Question.Subscribe(_typingView.SetQuestion).RegisterTo(_tokenSource.Token);
        }

        /// <summary>
        /// 問題を設定して入力を受け付け開始する
        /// </summary>
        public void StartTyping(string question)
        {
            _questionModel.SetQuestion(question);
            _answerModel.Clear();
            _typingInput.SetEnable(true);
        }

        /// <summary>
        /// 入力受付を止める
        /// </summary>
        public void StopTyping()
        {
            _typingInput.SetEnable(false);
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _answerModel?.Dispose();
            _questionModel?.Dispose();
            // _typingInput はコンテナが破棄するのでここでは触らない
        }

        private readonly AnswerModel _answerModel;
        private readonly QuestionModel _questionModel;
        private readonly ITypingInput _typingInput;
        private readonly ITypingView _typingView;
        private readonly CancellationTokenSource _tokenSource;
    }
}
