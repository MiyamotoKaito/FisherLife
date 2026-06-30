namespace Commons
{
    /// <summary>
    ///     攻撃計算のインターフェース。
    /// </summary>
    public interface IAttackCalculator
    {
        /// <summary>
        ///     体力と防御力をもとに攻撃を計算する。
        /// </summary>
        AttackResult Calculate(IAttacker attacker, IDamageable target);
    }
}
