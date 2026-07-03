using System.Collections.Generic;
using Commons;
using R3;
using UnityEngine;

namespace TypingModule
{
    /// <summary>
    ///     タイピングモードの出題と入力受付を制御するコントローラー。
    /// </summary>
    public class TypingController : IFishingModeController
    {
        /// <summary>
        ///     依存を受け取り、単語辞書を初期化する。
        /// </summary>
        public TypingController(IWordSeparatorUsecase wordSeparatorUsecase,
            TypingPresenter typingPresenter,
            TextAsset textAsset)
        {
            _wordSeparatorUsecase = wordSeparatorUsecase;
            _typingPresenter = typingPresenter;
            _textAsset = textAsset;
            Init();
        }

        /// <summary> レベルごとの単語辞書。 </summary>
        public Dictionary<uint, string[]> WordDictionary => _wordDictionary;
        /// <summary> 対応する釣りモード。 </summary>
        public FishingMode FishingMode => FishingMode.Typing;
        /// <summary> 対応するアクションマップ種別。 </summary>
        public InputActionMapType InputActionMapType => InputActionMapType.Typing;

        public Observable<Unit> OnAttack => _onAttack;

        /// <summary>
        ///     出題を開始する。
        /// </summary>
        public void Begin()
        {
            Start();
        }

        /// <summary>
        ///     出題を停止する。
        /// </summary>
        public void End()
        {
            _typingPresenter.StopTyping();
            _typingPresenter.Completed -= Next;
        }

        /// <summary>
        ///     破棄時に購読を解除する。
        /// </summary>
        public void Dispose()
        {
            if (_typingPresenter != null)
            {
                _typingPresenter.Completed -= Next;
            }
            _onAttack?.Dispose();
        }

        private const uint TARGET_LEVEL = 1;

        private readonly IWordSeparatorUsecase _wordSeparatorUsecase;
        private readonly TypingPresenter _typingPresenter;
        private readonly TextAsset _textAsset;
        private Dictionary<uint, string[]> _wordDictionary;
        private Subject<Unit> _onAttack;

        /// <summary>
        ///     CSVから単語辞書を生成する。
        /// </summary>
        private async void Init()
        {
            _wordDictionary = await _wordSeparatorUsecase.WordSeparate(_textAsset.text);

            // 対象レベルの単語が無い場合はエラーを出す。
            if (!_wordDictionary.ContainsKey(TARGET_LEVEL))
            {
                Debug.LogError($"[TypingController] レベル {TARGET_LEVEL} の単語が CSV に見つかりません。");
                return;
            }
            _onAttack = new();
        }

        /// <summary>
        ///     完了通知を購読し、最初の問題を出題する。
        /// </summary>
        private async void Start()
        {
            _typingPresenter.Completed += Next;
            Next();
        }

        /// <summary>
        ///     対象レベルからランダムに1問を出題する。
        /// </summary>
        private void Next()
        {
            Debug.Log($"[TypingController] レベル {TARGET_LEVEL} の単語からランダムに1問を出題します。");
            var words = _wordDictionary[TARGET_LEVEL];
            var word = words[UnityEngine.Random.Range(0, words.Length)];
            _typingPresenter.StartTyping(word);
            _onAttack.OnNext(Unit.Default);
        }
    }
}
