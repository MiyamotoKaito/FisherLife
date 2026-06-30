using Commons;
using UnityEngine;

namespace FishingModule
{
    public class AttackCalculator : IAttackCalculator
    {
        public AttackResult Calculate(IAttacker attacker, IDamageable target)
        {
            var isCritical = UnityEngine.Random.value < attacker.CriticalProbability / 100;
            var rawAttack = isCritical ? attacker.Power * attacker.CriticalMultiplier : attacker.Power;
            var damage = Mathf.Max(1, Mathf.RoundToInt(rawAttack) - target.Defence);
            return new AttackResult(damage, isCritical);
        }
    }
}
