using Commons;
using UnityEngine;

namespace FishingModule
{
    public class AttackPipeline : IAttackPipeline
    {
        public AttackPipeline(IAttackCalculator attackCalculator)
        {
            _attackCalculator = attackCalculator;
        }
        public void Attack(IAttacker attacker, IDamageable target)
        {
            var result = _attackCalculator.Calculate(attacker, target);
            target.TakeDamage(result.Damage);
            Debug.Log($"攻撃が命中しました。ダメージ: {result.Damage}\n残りHP: {target.Hp.CurrentValue}");
        }
        private readonly IAttackCalculator _attackCalculator;
    }
}
