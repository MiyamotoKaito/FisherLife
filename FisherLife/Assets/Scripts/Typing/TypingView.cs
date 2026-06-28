using Commons;
using TMPro;
using UnityEngine;
namespace TypingModule
{
    /// <summary>
    /// タイピングのUI管理のViewクラス
    /// </summary>
    public class TypingView : MonoBehaviour, ITypingView
    {
        public void SetQuestion(string q)
        {
            _question.text = q;
        }
        public void UpdateAnswer(string str)
        {
            _answer.text = str;
        }
        [SerializeField] private TextMeshProUGUI _answer;
        [SerializeField] private TextMeshProUGUI _question;
    }
}