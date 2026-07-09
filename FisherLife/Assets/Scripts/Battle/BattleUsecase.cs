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
        }
        public async UniTask<BattleResult> BattleStart(IAttacker attacker, IDamageable target, int level)
        {
            // 戦闘ごとに新しいCTSを作り、終了時にCancelして購読を確実に破棄する。
            // （使い回すと購読が次の戦闘へ残り、遷移中に幽霊攻撃が飛ぶ）
            using var cts = new CancellationTokenSource();

            _fishingModeRegistry.GetMode(FishingMode.Typing).OnAttack.
                Subscribe(_ => _attackPipeline.Attack(attacker, target)).
                RegisterTo(cts.Token);

            var dead = target.Hp.FirstAsync(hp => hp <= 0, cts.Token);
            var timeOut = UniTask.Delay((int)(_timeLimit * 1000 * level), cancellationToken: cts.Token);

            try
            {
                //　どちらかが先に完了するまで待機する
                await UniTask.WhenAny(dead.AsUniTask(), timeOut);
            }
            finally
            {
                // OnAttackの購読と待機を破棄する（次の戦闘へ持ち越さない）。
                cts.Cancel();
            }

            return target.Hp.Value <= 0 ? BattleResult.Caught : BattleResult.Escaped;
        }
        private readonly float _timeLimit;
        private readonly IAttackPipeline _attackPipeline;
        private readonly IFishingModeRegistry _fishingModeRegistry;
    }
}
