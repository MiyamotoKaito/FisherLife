namespace Commons
{
    /// <summary>
    ///     釣りコントローラーのインターフェース。
    /// </summary>
    public interface IFishingController : IController
    {
        void ReturnToFishingState(BattleResult result);
    }
}
