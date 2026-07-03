using UnityEngine;

namespace Commons
{
    public interface IFishFactory
    {
        IFish CreateFish(IRod rod);
        void HideFish();
    }
}
