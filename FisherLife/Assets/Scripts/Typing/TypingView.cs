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

        /// <summary>
        ///     タイピング画面（背景＋テキスト）の表示/非表示を切り替える。
        /// </summary>
        public void SetVisible(bool visible)
        {
            _panel.SetActive(visible);
        }

        [SerializeField, Tooltip("背景＋テキストをまとめたパネル。初期は非アクティブにしておく。")]
        private GameObject _panel;

        [SerializeField, Tooltip("解答を表示するテキスト。")]
        private TextMeshProUGUI _answer;

        [SerializeField, Tooltip("問題文を表示するテキスト。")]
        private TextMeshProUGUI _question;
    }
}
