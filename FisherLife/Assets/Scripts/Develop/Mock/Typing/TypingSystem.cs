using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Miyamoto.FisherLife.Develop.Mock.Typing
{
    /// <summary>
    /// タイピングシステム
    /// </summary>
    public class TypingSystem : MonoBehaviour
    {
        [SerializeField] private AssetReferenceT<TextAsset> _textAssetReference;
        private LoadWords _loadWords;
        private AsyncOperationHandle<TextAsset> _handle;

        private void Awake()
        {
            TextLoadAsync().Forget();
        }
        
        private async UniTask TextLoadAsync()
        {
            _handle = Addressables.LoadAssetAsync<TextAsset>(_textAssetReference);
            await _handle.Task;
            var textAsset = _handle.Result;
            _loadWords = new LoadWords(textAsset);
        }
    }
}