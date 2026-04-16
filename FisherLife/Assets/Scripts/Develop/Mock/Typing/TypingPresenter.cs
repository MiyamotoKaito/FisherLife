using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Miyamoto.FisherLife.Develop.Mock.Typing
{
    /// <summary>
    /// タイピングのプレゼンター
    /// </summary>
    public class TypingPresenterMock : MonoBehaviour
    {
        [SerializeField] private AssetReferenceT<TextAsset> _textAssetReference;
        [SerializeField] private int _level;

        private LoadWords _loadWords;
        private AsyncOperationHandle<TextAsset> _handle;
        private TypingModel _model;
        private TypingView _view;
        private TypingInput _input;

        private void OnEnable()
        {
            _input = GetComponent<TypingInput>();
            _view = GetComponent<TypingView>();
            _model = new TypingModel();
            TextLoadAsync().Forget();

            _input.OnType += GetText;
            _input.OnBackspace += Answer;
            _input.OnEnter += Answer;
        }

        /// <summary>
        /// 単語を非同期でロードする
        /// </summary>
        private async UniTask TextLoadAsync()
        {
            _handle = Addressables.LoadAssetAsync<TextAsset>(_textAssetReference);
            await _handle.Task;
            var textAsset = _handle.Result;
            _loadWords = new LoadWords(textAsset);
            GetRandomQuestion();
        }

        /// <summary>
        /// InoutSystemから受け取った単語をTMPのテキストに追加する
        /// </summary>
        /// <param name="text"></param>
        private void GetText(string text)
        {
            _model.AddText(text);
            _view.AddText(_model.TypingText);
        }

        private void DeleteText()
        {
        }

        /// <summary>
        /// 答えと文字列が合っているか確認する
        /// </summary>
        private void Answer()
        {
            if (_view.Question == _view.TypingText)
            {
                Debug.Log("正解");

                _view.DeleteAllTypes();
                _view.DeleteQuestion();
                _view.AddQuestion(_view.TypingText);
            }

            Debug.Log("不正解");
        }

        /// <summary>
        /// 問題を取得
        /// </summary>
        private void GetRandomQuestion()
        {
            var question = _loadWords.WordDictionary[_level][Random.Range(0, _loadWords.WordDictionary.Count)];

            _view.AddQuestion(question);
        }
    }
}