using System.Collections.Generic;
using UnityEngine;

namespace PlayerModule
{
    /// <summary>
    /// 釣り竿のパラメータアセットをまとめるSOクラス
    /// </summary>
    [CreateAssetMenu(fileName = "RodListAsset", menuName = "Scriptable Objects/RodListAsset")]
    public class RodListAsset : ScriptableObject
    {
        /// <summary> 釣り竿のリスト。 </summary>
        public IReadOnlyList<RodParameter> RodParameters => _parameters;

        [SerializeField, Tooltip("釣り竿のリスト")]
        private List<RodParameter> _parameters;
    }
}
