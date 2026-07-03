namespace Commons
{
    public interface IFishingModeRegistry
    {
        void AddMode(IFishingModeController mode);
        IFishingModeController GetMode(FishingMode mode);
    }
}
