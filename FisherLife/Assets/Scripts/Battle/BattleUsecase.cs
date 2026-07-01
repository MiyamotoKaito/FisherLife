using System;
using System.Threading;
using Commons;
using Cysharp.Threading.Tasks;
using R3;

namespace BattleModule
{
    public class BattleUsecase
    {
        public BattleUsecase(IAttackPipeline attackPipeline,
            float timeLimit,
            IWorldStateMachine worldStateMachine)
        {
            _attackPipeline = attackPipeline;
            _timeLimit = timeLimit;
            _worldStateMachine = worldStateMachine;
            _cancellationTokenSource = new CancellationTokenSource();
        }
        public async UniTask<BattleResult> BattleStart(IAttacker attacker, IDamageable target)
        {
            using var _ = _fishingModeController.OnAttack.
                Subscribe(_ => _attackPipeline.Attack(attacker, target)).
                RegisterTo(_cancellationTokenSource.Token);

            var dead = target.Hp.FirstAsync(hp => hp <= 0);
            var timeOut = UniTask.Delay(TimeSpan.FromSeconds(_timeLimit));

            await UniTask.WhenAny(dead.AsUniTask(), timeOut);
            _worldStateMachine.BackState();
            return target.Hp.Value <= 0 ? BattleResult.Caught : BattleResult.Escaped;
        }
        private readonly float _timeLimit;
        private readonly IWorldStateMachine _worldStateMachine;
        private readonly IAttackPipeline _attackPipeline;
        private readonly IFishingModeController _fishingModeController;
        private readonly CancellationTokenSource _cancellationTokenSource;
    }
}
