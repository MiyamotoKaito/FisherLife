using Common;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using VContainer;

namespace TypingModule
{
    public class TypingController : MonoBehaviour
    {
        public Dictionary<uint, string[]> WordDictionary => _wordDictionary;
        private Dictionary<uint, string[]> _wordDictionary;

        [SerializeField]
        private TextAsset _textAsset;
        [SerializeField]
        private uint _level;
        private IWordSeparatorUsecase _wordSeparatorUsecase;
        private TypingPresenter _typingPresenter;

        [Inject]
        public void Inject(IWordSeparatorUsecase wordSeparatorUsecase, TypingPresenter typingPresenter)
        {
            _wordSeparatorUsecase = wordSeparatorUsecase;
            _typingPresenter = typingPresenter;
        }

        private async void Start()
        {
            _typingPresenter.Completed += Next;

            _wordDictionary = await _wordSeparatorUsecase.WordSeparate(_textAsset.text);

            if (!_wordDictionary.ContainsKey(_level))
            {
                Debug.LogError($"[TypingController] レベル {_level} の単語が CSV に見つかりません。");
                return;
            }

            Next();
            WorldStateMachine.Instance.ChangeState(WorldStateType.Typing);
        }

        private void OnDestroy()
        {
            if (_typingPresenter != null)
                _typingPresenter.Completed -= Next;
        }

        private void Next()
        {
            var words = _wordDictionary[_level];               // 辞書の実配列を使う
            var word = words[UnityEngine.Random.Range(0, words.Length)];
            _typingPresenter.StartTyping(word);
        }
    }
}
