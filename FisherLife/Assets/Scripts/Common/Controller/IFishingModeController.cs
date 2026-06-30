namespace Commons
{
    /// <summary>
    ///     釣りモードごとのコントローラーのインターフェース。
    /// </summary>
    public interface IFishingModeController : IController
    {
        /// <summary> 対応する釣りモード。 </summary>
        FishingMode FishingMode { get; }
    }
}
