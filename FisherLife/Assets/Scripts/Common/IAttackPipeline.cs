using System;

namespace Commons
{
    /// <summary>
    ///     攻撃処理のパイプラインのインターフェース。
    /// </summary>
    public interface IAttackPipeline
    {
        /// <summary>
        ///     攻撃側の能力をもとに、対象へ攻撃を計算して適用する。
        /// </summary>
        void Attack(IAttacker attacker, IDamageable target);
    }
}
