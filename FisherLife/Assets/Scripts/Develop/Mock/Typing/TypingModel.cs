using System.Text;

namespace Miyamoto.FisherLife.Develop.Mock.Typing
{
    /// <summary>
    /// タイピングModel
    /// </summary>
    public class TypingModel
    {
        public string TypingText => _typedText;

        public string ConversionText => _conversionText;

        private string _typedText;
        private string _conversionText;
        private StringBuilder _mergeText;

        public TypingModel()
        {
            _mergeText = new StringBuilder();
        }

        public void AddText(string text)
        {
            _mergeText.Append(text);
            _typedText = _mergeText.ToString();
        }

        public void DeleteText()
        {
            _mergeText.Remove(_mergeText.Length - 1, 1);
            _typedText = _mergeText.ToString();
        }
    }
}