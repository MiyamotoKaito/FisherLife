using System.Threading;
using Commons;
using Cysharp.Threading.Tasks;
using R3;

namespace BattleModule
{
    public class BattleUsecase : IBattleUsecase
    {
        public BattleUsecase(IAttackPipeline attackPipeline,
            IWorldStateMachine worldStateMachine,
            IFishingModeRegistry fishingModeRegistry)
        {
            _attackPipeline = attackPipeline;
            _timeLimit = 10;
            _fishingModeRegistry = fishingModeRegistry;
            _cancellationTokenSource = new CancellationTokenSource();
        }
        public async UniTask<BattleResult> BattleStart(IAttacker attacker, IDamageable target)
        {
            using var _ = _fishingModeRegistry.GetMode(FishingMode.Typing).OnAttack.
                Subscribe(_ => _attackPipeline.Attack(attacker, target)).
                RegisterTo(_cancellationTokenSource.Token);

            var dead = target.Hp.FirstAsync(hp => hp <= 0, _cancellationTokenSource.Token);
            var timeOut = UniTask.Delay((int)(_timeLimit * 1000), cancellationToken: _cancellationTokenSource.Token);

            //　どちらかが先に完了するまで待機する
            await UniTask.WhenAny(dead.AsUniTask(), timeOut);
            return target.Hp.Value <= 0 ? BattleResult.Caught : BattleResult.Escaped;
        }
        private readonly float _timeLimit;
        private readonly IAttackPipeline _attackPipeline;
        private readonly IFishingModeRegistry _fishingModeRegistry;
        private readonly CancellationTokenSource _cancellationTokenSource;
    }
}
