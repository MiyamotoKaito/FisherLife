using TMPro;
using UnityEngine;
namespace TypingModule
{
    /// <summary>
    /// タイピングのUI管理のViewクラス
    /// </summary>
    public class TypingView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _answer;
        [SerializeField] private TextMeshProUGUI _question;
    }
}