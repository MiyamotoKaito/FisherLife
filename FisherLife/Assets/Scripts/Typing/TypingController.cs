using Commons;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace TypingModule
{
    public class TypingController : IFishingModeController
    {
        public Dictionary<uint, string[]> WordDictionary => _wordDictionary;

        public FishingMode FishingMode => FishingMode.Typing;

        public InputActionMapType InputActionMapType => InputActionMapType.Typing;

        private Dictionary<uint, string[]> _wordDictionary;

        private TextAsset _textAsset;
        private uint _level;

        private IWordSeparatorUsecase _wordSeparatorUsecase;
        private TypingPresenter _typingPresenter;
        private IWorldStateMachine _worldStateMachine;

        [Inject]
        public TypingController(IWordSeparatorUsecase wordSeparatorUsecase,
            TypingPresenter typingPresenter,
            IWorldStateMachine worldStateMachine,
            TextAsset textAsset)
        {
            _wordSeparatorUsecase = wordSeparatorUsecase;
            _typingPresenter = typingPresenter;
            _worldStateMachine = worldStateMachine;
            _textAsset = textAsset;
            Init();
        }
        public void Enable()
        {
            Start();
        }

        public void Disable()
        {
            _typingPresenter.Completed -= Next;
        }
        private async void Start()
        {
            _typingPresenter.Completed += Next;
            Next();
            _worldStateMachine.ChangeState(WorldStateType.Typing);
        }
        public void Dispose()
        {
            if (_typingPresenter != null)
                _typingPresenter.Completed -= Next;
        }
        private async void Init()
        {
            _wordDictionary = await _wordSeparatorUsecase.WordSeparate(_textAsset.text);

            if (!_wordDictionary.ContainsKey(_level))
            {
                Debug.LogError($"[TypingController] レベル {_level} の単語が CSV に見つかりません。");
                return;
            }
        }
        private void Next()
        {
            var words = _wordDictionary[_level];               // 辞書の実配列を使う
            var word = words[UnityEngine.Random.Range(0, words.Length)];
            _typingPresenter.StartTyping(word);
        }
    }
}
