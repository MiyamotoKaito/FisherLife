using R3;

namespace Commons
{
    /// <summary>
    ///     ダメージを受けられる対象のインターフェース。
    /// </summary>
    public interface IDamageable
    {
        /// <summary> 体力。 </summary>
        ReactiveProperty<int> Hp { get; }
        /// <summary> 防御力。 </summary>
        int Defence { get; }

        /// <summary>
        ///     ダメージを受ける。
        /// </summary>
        void TakeDamage(int damage);
    }
}
