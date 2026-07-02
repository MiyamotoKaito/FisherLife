using UnityEngine;

namespace FishModule
{
    /// <summary>
    ///     魚のパラメータを保持するデータコンテナ。
    /// </summary>
    [CreateAssetMenu(fileName = "FishParamater", menuName = "Scriptable Objects/FishParamater")]
    public class FishParameter : ScriptableObject
    {
        /// <summary> レベル。 </summary>
        public byte Level => _level;
        /// <summary> 名前。 </summary>
        public string Name => _name;
        /// <summary> 体力。 </summary>
        public int Hp => _hp;
        /// <summary> 防御力。 </summary>
        public int Defence => _defence;
        /// <summary> 入手できる金額。 </summary>
        public int MoneyAmount => _moneyAmount;

        [Header("レベル")]
        [SerializeField, Tooltip("レベル。")]
        private byte _level;

        [Header("表示関連")]
        [SerializeField, Tooltip("名前。")]
        private string _name;

        [Header("パラメータ設定")]
        [SerializeField, Tooltip("体力。")]
        private int _hp;

        [SerializeField, Tooltip("防御力。")]
        private int _defence;

        [SerializeField, Tooltip("入手できる金額。")]
        private int _moneyAmount;
    }
}
