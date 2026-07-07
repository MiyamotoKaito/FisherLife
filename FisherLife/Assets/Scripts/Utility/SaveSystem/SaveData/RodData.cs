namespace Utility
{
    /// <summary>
    ///     釣り竿のセーブデータ
    /// </summary>
    [System.Serializable]
    public class RodData : SaveBase
    {
        public string RodName;
        public byte RodLevel;
        public int AttackPower;
        public float CriticalMultiplier;
        public float CriticalRate;
    }
}