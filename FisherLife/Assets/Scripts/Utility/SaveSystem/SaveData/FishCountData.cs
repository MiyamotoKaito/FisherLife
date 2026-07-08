using System.Collections.Generic;

namespace Utility
{
    /// <summary>
    /// 所持している魚の数(種類ごと)のセーブデータ
    /// </summary>
    [System.Serializable]
    public class FishCountData : SaveBase
    {
        // Dictionaryの保存は6.5に移行してから
        public List<FishCountEntry> Fishes = new();
    }
}
