namespace Utility
{
    /// <summary>
    /// 魚の名前と数を保存するセーブデータ
    /// </summary>
    [System.Serializable]
    public class FishCountEntry
    {
        public string FishName;
        public int Count;
        /// <summary> 一度でも入手したか（図鑑用。売って数が0になっても残る）。 </summary>
        public bool IsObtained;
    }
}
