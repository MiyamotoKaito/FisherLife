using TMPro;
using UnityEngine;

namespace Miyamoto.FisherLife.Develop.Mock.Typing
{
    public class TypingViewMock : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _typingText;

        /// <summary>
        /// 文字追加
        /// </summary>
        /// <param name="text"></param>
        public void AddText(string text)
        {
            _typingText.text += text;
        }
        /// <summary>
        /// 一文字削除
        /// </summary>
        public void DeleteText()
        {
            _typingText.text.Remove(_typingText.text.Length - 1,1);
        }
    }
}