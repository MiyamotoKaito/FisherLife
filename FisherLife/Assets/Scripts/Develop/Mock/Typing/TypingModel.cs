using UnityEngine;

public class TypingModel
{
    public string Question => _question;
    public string Answer => _answer;

    private string _question;
    private string _answer;

    public void AddChar(char c)
    {
        _answer += c;
    }

    public void RemoveChar()
    {
        _answer.Remove(_answer.Length - 1);
    }
}
