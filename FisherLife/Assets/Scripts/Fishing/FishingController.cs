using Commons;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace FishingModule
{
    /// <summary>
    ///     釣りを制御するコントローラー。
    /// </summary>
    public class FishingController : IFishingController
    {
        public FishingController(
            IFishFactory fishFactory,
            IRod rod,
            IWorldStateMachine worldStateMachine,
            IBattleUsecase battleUsecase)
        {
            _fishFactory = fishFactory;
            _rod = rod;
            _worldStateMachine = worldStateMachine;
            _battleUsecase = battleUsecase;
        }
        public InputActionMapType InputActionMapType => InputActionMapType.Fishing;
        public void Begin()
        {
            if (_battleResult == BattleResult.None)
            {
                RunAsync().Forget();
            }
            else
            {
                ReturnToFishingState(_battleResult);
                _battleResult = BattleResult.None;
            }
        }

        public void Dispose()
        {
           
        }

        public void End()
        {
            
        }
        /// <summary>
        ///     戦闘結果に応じて釣り状態に戻る。
        /// </summary>
        /// <param name="result"></param>
        public void ReturnToFishingState(BattleResult result)
        {
            if (result == BattleResult.Caught)
            {
                Debug.Log($"魚を釣りました！");
            }
            else if (result == BattleResult.Escaped)
            {
                Debug.Log($"魚が逃げました。");
            }
        }
        /// <summary>
        ///     釣りの非同期処理を実行する。
        /// </summary>
        /// <returns></returns>
        private async UniTask RunAsync()
        {
            var target = _fishFactory.CreateFish(_rod);
            // 魚が釣れるまで待つ
            await UniTask.Delay(5000);
            // 魚が釣れたら戦闘状態に遷移する
            _fishFactory.HideFish();
            _worldStateMachine.ChangeState(WorldStateType.Typing);
            // 戦闘開始
            var result = await _battleUsecase.BattleStart(_rod, target);
            _battleResult = result;
            // 戦闘終了後、釣り状態に戻る
            _worldStateMachine.BackState();
        }
        private readonly IBattleUsecase _battleUsecase;
        private readonly IWorldStateMachine _worldStateMachine;
        private readonly IFishFactory _fishFactory;
        private readonly IRod _rod;
        private BattleResult _battleResult = BattleResult.None;
    }
}
