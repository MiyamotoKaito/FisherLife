namespace Commons
{
    /// <summary>
    ///     ワールド状態の1つを表すインターフェース。
    /// </summary>
    public interface IState
    {
        /// <summary> 対応するワールド状態種別。 </summary>
        WorldStateType WorldState { get; }

        /// <summary>
        ///     状態に入るときの処理を行う。
        /// </summary>
        void Entry();

        /// <summary>
        ///     状態から出るときの処理を行う。
        /// </summary>
        void Exit();
    }
}
