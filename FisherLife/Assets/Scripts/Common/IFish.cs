namespace Commons
{
    /// <summary>
    ///     魚のインターフェース。
    /// </summary>
    public interface IFish : IDamageable
    {
        /// <summary> 名前。 </summary>
        string Name { get; }
        /// <summary> 入手できる金額。 </summary>
        int MoneyAmount { get; }
        /// <summary> レベル。 </summary>
        int Level { get; }
    }
}
