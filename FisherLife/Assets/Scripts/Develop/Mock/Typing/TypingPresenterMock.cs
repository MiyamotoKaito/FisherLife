using Cysharp.Threading.Tasks;
using Miyamoto.FisherLife.Develop.Input;
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

        private LoadWords _loadWords;
        private AsyncOperationHandle<TextAsset> _handle;
        private TypingModelMock _modelMock;
        private TypingViewMock _viewMock;
        private InputActions _input;
        private void Start()
        {
            _input = GetComponent<InputActions>();
            _viewMock = GetComponent<TypingViewMock>();
            _modelMock = new TypingModelMock();
            TextLoadAsync().Forget();
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
        }
    }
}