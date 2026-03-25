using UnityEngine;

namespace Miyamoto.FisherLife.Develop.Mock.Typing
{
    /// <summary>
    /// タイピングシステム
    /// </summary>
    public class TypingSystem : MonoBehaviour
    {
        [SerializeField] private TextAsset _textAsset;
        private LoadWords _loadWords;

        private void Awake()
        {
            _loadWords = new LoadWords(_textAsset);
        }
    }
}