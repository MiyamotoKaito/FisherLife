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
        private async void Start()
        {
            _wordDictionary = await _wordSeparatorUsecase.WordSeparate(_textAsset.text);
            Next();
            WorldStateMachine.Instance.ChangeState(WorldStateType.Typing);
        }
        private void OnEnable()
        {
            _typingPresenter.Completed += Next;
        }
        private void OnDisable()
        {
            _typingPresenter.Completed -= Next;
        }
        [Inject]
        public async void Inject(IWordSeparatorUsecase wordSeparatorUsecase, TypingPresenter typingPresenter)
        {
            _wordSeparatorUsecase = wordSeparatorUsecase;
            _typingPresenter = typingPresenter;
        }
        private void Next()
        {
            var words = new string[_wordDictionary[_level].Length];
            var word = words[UnityEngine.Random.Range(0, words.Length)];
            _typingPresenter.StartTyping(word);
        }
    }
}
