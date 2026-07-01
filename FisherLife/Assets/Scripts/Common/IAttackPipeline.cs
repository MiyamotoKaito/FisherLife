using System;

namespace Commons
{
    /// <summary>
    ///     攻撃処理のパイプラインのインターフェース。
    /// </summary>
    public interface IAttackPipeline : IDisposable
    {
        /// <summary>
        ///     攻撃側の能力をもとに、対象へ攻撃を計算して適用する。
        /// </summary>
        AttackResult Attack(IAttacker attacker, IDamageable target);
    }
}
