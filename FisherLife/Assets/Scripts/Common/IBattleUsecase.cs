using Cysharp.Threading.Tasks;

namespace Commons
{
    public interface IBattleUsecase
    {
        UniTask<BattleResult> BattleStart(IAttacker attacker, IDamageable target);
    }
}
