namespace Commons
{
    /// <summary>
    ///     ワールド状態を管理するステートマシンのインターフェース。
    /// </summary>
    public interface IWorldStateMachine
    {
        /// <summary>
        ///     状態を登録する。
        /// </summary>
        void AddState(IState state);

        /// <summary>
        ///     指定した状態へ遷移する。
        /// </summary>
        void ChangeState(WorldStateType worldStateType);

        /// <summary>
        ///     直前の状態へ戻る。
        /// </summary>
        void BackState();
    }
}
