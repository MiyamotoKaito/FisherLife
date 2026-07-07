using UnityEditor.MPE;
using UnityEngine;

namespace PlayerModule
{
    /// <summary>
    /// 釣り竿のSO
    /// </summary>
    [CreateAssetMenu(fileName = "RodParameter", menuName = "Scriptable Objects/RodParameter")]
    public class RodParameter : ScriptableObject
    {
        /// <summary> 釣り竿の名前。 </summary>
        public string Name => _name;
        /// <summary> 釣り竿のレベル。 </summary>
        public byte Level => _level;
        /// <summary> 攻撃力。 </summary>
        public int AttackPower => _attackPower;
        /// <summary> クリティカル倍率。 </summary>
        public float CriticalMutiplier => _criticalMultiplier;
        /// <summary> クリティカル発生倍率。 </summary>
        public float CriticalRate => _criticalRate;

        [SerializeField, Tooltip("釣り竿の名前")]
        private string _name;
        [SerializeField, Tooltip("釣り竿のレベル")]
        private byte _level;
        [Header("パラメーター設定")]
        [SerializeField, Tooltip("釣り竿の攻撃力")]
        private int _attackPower;
        [SerializeField, Tooltip("クリティカル倍率")]
        private float _criticalMultiplier;
        [SerializeField, Tooltip("クリティカル発生倍率"),Range(1,100)]
        private float _criticalRate;
    }
}
