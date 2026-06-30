namespace Commons
{
    /// <summary>
    ///     タイピング画面の表示を担うインターフェース。
    /// </summary>
    public interface ITypingView
    {
        /// <summary>
        ///     問題文を設定する。
        /// </summary>
        void SetQuestion(string question);

        /// <summary>
        ///     解答欄の表示を更新する。
        /// </summary>
        void UpdateAnswer(string answer);
    }
}
