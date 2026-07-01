using Commons;

namespace FishingModule
{
    public class AttackPipeline : IAttackPipeline
    {
        public AttackPipeline(IAttackCalculator attackCalculator)
        {
            _attackCalculator = attackCalculator;
        }
        public AttackResult Attack(IAttacker attacker, IDamageable target)
        {
            var result = _attackCalculator.Calculate(attacker, target);
            target.TakeDamage(result.Damage);
            return result;
        }

        private readonly IAttackCalculator _attackCalculator;
    }
}
