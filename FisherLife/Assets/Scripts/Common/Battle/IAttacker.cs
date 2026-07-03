namespace Commons
{
    /// <summary>
    ///     攻撃する側のインターフェース。攻撃力やクリティカル能力を持つ。
    /// </summary>
    public interface IAttacker
    {
        /// <summary> 攻撃力。 </summary>
        int Power { get; }
        /// <summary> クリティカル倍率。 </summary>
        float CriticalMultiplier { get; }
        /// <summary> クリティカル発生確率。 </summary>
        float CriticalProbability { get; }
    }
}
