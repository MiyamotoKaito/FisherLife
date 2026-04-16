using TMPro;
using UnityEngine;

namespace Miyamoto.FisherLife.Develop.Mock.Typing
{
    public class TypingView : MonoBehaviour
    {
        public string Question => _questionText.text;
        public string TypingText => _typingText.text;

        [SerializeField] private TextMeshProUGUI _questionText;
        [SerializeField] private TextMeshProUGUI _typingText;

        /// <summary>
        /// 文字追加
        /// </summary>
        /// <param name="text"></param>
        public void AddText(string text)
        {
            _typingText.text = text;
        }

        /// <summary>
        /// 一文字削除
        /// </summary>
        public void DeleteText()
        {
            _typingText.text.Remove(_typingText.text.Length - 1, 1);
        }

        public void AddQuestion(string question)
        {
            _questionText.text += question;
        }

        public void DeleteQuestion()
        {
            _questionText.text = string.Empty;
        }

        public void DeleteAllTypes()
        {
            _typingText.text = string.Empty;
        }

        private void Awake()
        {
            _typingText.text = string.Empty;
            _questionText.text = string.Empty;
        }
    }
}