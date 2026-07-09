using Commons;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility;

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
            IBattleUsecase battleUsecase,
            IPlayerFishingAnimation playerFishingAnimation,
            CatchResultPanel catchResultPanel,
            InputActionAsset inputActions,
            IFishingModeRegistry fishingModeRegistry)
        {
            _fishFactory = fishFactory;
            _rod = rod;
            _worldStateMachine = worldStateMachine;
            _battleUsecase = battleUsecase;
            _playerFishingAnimation = playerFishingAnimation;
            _catchResultPanel = catchResultPanel;
            _fishingModeRegistry = fishingModeRegistry;

            // 同名プロパティ（InputActionMapType.Fishing）の文字列でマップを取得する。
            _fishingActionMap = inputActions.FindActionMap(InputActionMapType.ToString(), true);
            _entryAction = _fishingActionMap.FindAction(ENTRY_ACTION);
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
                _playerFishingAnimation.GetFish();
                Debug.Log($"魚を釣りました！");
                // 釣果パネルを出し、Space入力で前のStateへ戻る。
                ShowResultAsync(_currentFish).Forget();
            }
            else
            {
                _playerFishingAnimation.Stop();
                Debug.Log($"魚が逃げました。");
                _worldStateMachine.BackState();
            }
        }

        /// <summary>
        ///     釣果パネルを表示し、Space入力を待ってから前のStateへ戻る。
        /// </summary>
        private async UniTaskVoid ShowResultAsync(IFish fish)
        {
            _catchResultPanel.Show(fish);

            await WaitForEntryAsync();

            _catchResultPanel.Hide();
            _worldStateMachine.BackState();
        }

        /// <summary>
        ///     Fishingの Entry アクションが発火するまで待つ。
        /// </summary>
        private async UniTask WaitForEntryAsync()
        {
            if (_entryAction == null) return; // 未設定なら待たない

            var tcs = new UniTaskCompletionSource();
            void Handler(InputAction.CallbackContext _) => tcs.TrySetResult();

            _entryAction.started += Handler;
            try
            {
                await tcs.Task;
            }
            finally
            {
                _entryAction.started -= Handler;
            }
        }
        /// <summary>
        ///     釣りの非同期処理を実行する。
        /// </summary>
        /// <returns></returns>
        private async UniTask RunAsync()
        {
            // プレイヤーをスポットへ向けてから投げる。
            _playerFishingAnimation.FaceTo(_fishFactory.SpotPosition);
            _playerFishingAnimation.Throw();
            _currentFish = await _fishFactory.CreateFish(_rod, _playerFishingAnimation.FacingDirection);

            // 魚が釣れるまで待つ
            await UniTask.Delay(5000);

            // 魚が釣れたら戦闘状態に遷移する
            _fishFactory.HideFish();

            // 魚のレベルに応じてタイピングの単語レベルを設定する（遷移前）。
            _fishingModeRegistry.GetMode(FishingMode.Typing)?.SetLevel(_currentFish.Level);

            _worldStateMachine.ChangeState(WorldStateType.Typing);

            // 戦闘開始（戦闘中はループVFXを流す）
            _playerFishingAnimation.Fighting();
            _fishFactory.StartBattleVfx();
            var result = await _battleUsecase.BattleStart(_rod, _currentFish, _currentFish.Level);
            _fishFactory.StopBattleVfx();
            _battleResult = result;

            // 釣り上げた魚を所持データへ保存する。
            if (result == BattleResult.Caught)
            {
                await SaveCaughtFishAsync(_currentFish);
            }

            // 戦闘終了後、釣り状態に戻る
            _worldStateMachine.BackState();
        }

        /// <summary>
        ///     釣り上げた魚を所持数へ加算し、入手済みにして保存する。
        /// </summary>
        private async UniTask SaveCaughtFishAsync(IFish fish)
        {
            var data = await SaveSystem.LoadAsync<FishCountData>();

            var entry = data.Fishes.Find(f => f.FishName == fish.Name);
            if (entry == null)
            {
                data.Fishes.Add(new FishCountEntry
                {
                    FishName = fish.Name,
                    Count = 1,
                    IsObtained = true,
                });
            }
            else
            {
                entry.Count++;
                entry.IsObtained = true;
            }

            await SaveSystem.SaveAsync<FishCountData>();
        }
        private const string ENTRY_ACTION = "Entry";
        private readonly IBattleUsecase _battleUsecase;
        private readonly IWorldStateMachine _worldStateMachine;
        private readonly IFishFactory _fishFactory;
        private readonly IRod _rod;
        private readonly IPlayerFishingAnimation _playerFishingAnimation;
        private readonly CatchResultPanel _catchResultPanel;
        private readonly IFishingModeRegistry _fishingModeRegistry;
        private readonly InputActionMap _fishingActionMap;
        private readonly InputAction _entryAction;
        private BattleResult _battleResult = BattleResult.None;
        private IFish _currentFish;
    }
}
