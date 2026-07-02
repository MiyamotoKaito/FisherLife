using System.Collections.Generic;
using Commons;

namespace FishingModule
{
    public class FishingModeRegistry : IFishingModeRegistry
    {
        public void AddMode(IFishingModeController mode)
        {
            _modeControllers[mode.FishingMode] = mode;
        }

        public IFishingModeController GetMode(FishingMode mode)
        {
            return _modeControllers.GetValueOrDefault(mode);
        }

        private Dictionary<FishingMode, IFishingModeController> _modeControllers = new();
    }
}
