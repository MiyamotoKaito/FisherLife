using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Miyamoto.FisherLife.Develop.Mock.Typing
{
    /// <summary>
    /// タイピングModel
    /// </summary>
    public class TypingModel
    {
        private AssetReferenceT<TextAsset> _textAssetReference;
        private AsyncOperationHandle<TextAsset> _handle;


        public TypingModel(AssetReferenceT<TextAsset> textAsset)
        {
            _textAssetReference = textAsset;
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