using R3;

namespace Commons
{
    /// <summary>
    ///     釣りモードごとのコントローラーのインターフェース。
    /// </summary>
    public interface IFishingModeController : IController
    {
        /// <summary> 対応する釣りモード。 </summary>
        FishingMode FishingMode { get; }
        Observable<Unit> OnAttack { get; }
        /// <summary> 出題する難易度レベルを設定する（魚のレベルに連動）。 </summary>
        void SetLevel(int level);
    }
}
