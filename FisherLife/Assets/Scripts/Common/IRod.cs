namespace Commons
{
    /// <summary>
    ///     釣り竿のインターフェース。
    /// </summary>
    public interface IRod
    {
        /// <summary> 攻撃力。 </summary>
        int Power { get; }
        /// <summary> クリティカル倍率。 </summary>
        float CriticalMultiplier { get; }
        /// <summary> クリティカル発生確率。 </summary>
        float CriticalProbability { get; }
    }
}
