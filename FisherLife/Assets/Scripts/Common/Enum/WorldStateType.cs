namespace Commons
{
    /// <summary>
    ///     ワールド全体の状態種別。
    /// </summary>
    public enum WorldStateType
    {
        /// <summary> 未設定。 </summary>
        None = 0,
        /// <summary> タイピング中。 </summary>
        Typing = 1,
        /// <summary> 釣り中。 </summary>
        Fishing = 2,
        /// <summary> 移動中。 </summary>
        Moving = 3,
        /// <summary> メニュー表示中。 </summary>
        Menu = 4,
        /// <summary> ゲーム外。 </summary>
        OutGame = 5,
    }
}
