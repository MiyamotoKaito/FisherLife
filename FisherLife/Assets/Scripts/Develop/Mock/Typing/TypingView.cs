using TMPro;
using UnityEngine;

public class TypingView : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _question;
    [SerializeField] private TextMeshProUGUI _answer;

    private void Awake()
    {
        _question.text = string.Empty;
        _answer.text = string.Empty;
    }
    /// <summary>
    /// 問題のテキストを設定する
    /// </summary>
    /// <param name="question"></param>
    public void SetQuestion(string question)
    {
        _question.text = question;
    }
    /// <summary>
    /// 回答のテキストを設定する
    /// </summary>
    /// <param name="answer"></param>
    public void SetAnswer(string answer)
    {
        _answer.text = answer;
    }
}
