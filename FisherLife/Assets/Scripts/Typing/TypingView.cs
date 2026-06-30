using Commons;
using TMPro;
using UnityEngine;

namespace TypingModule
{
    /// <summary>
    ///     タイピングのUIを管理するView。
    /// </summary>
    public class TypingView : MonoBehaviour, ITypingView
    {
        /// <summary>
        ///     問題文を設定する。
        /// </summary>
        public void SetQuestion(string question)
        {
            _question.text = question;
        }

        /// <summary>
        ///     解答欄の表示を更新する。
        /// </summary>
        public void UpdateAnswer(string answer)
        {
            _answer.text = answer;
        }

        [SerializeField, Tooltip("解答を表示するテキスト。")]
        private TextMeshProUGUI _answer;

        [SerializeField, Tooltip("問題文を表示するテキスト。")]
        private TextMeshProUGUI _question;
    }
}
