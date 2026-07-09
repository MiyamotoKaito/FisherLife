using System.Collections.Generic;

namespace Utility
{
    public class RodCountData : SaveBase
    {
        public List<RodData> RodList = new();
        /// <summary> 装備中の竿の名前。 </summary>
        public string EquippedRodName;
    }
}
