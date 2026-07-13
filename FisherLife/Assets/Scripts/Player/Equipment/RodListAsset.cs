using System.Collections.Generic;
using Commons;
using UnityEngine;

namespace PlayerModule
{
    /// <summary>
    /// 釣り竿のパラメータアセットをまとめるSOクラス
    /// </summary>
    [CreateAssetMenu(fileName = "RodListAsset", menuName = "Scriptable Objects/RodListAsset")]
    public class RodListAsset : ScriptableObject, IRodCatalog
    {
        /// <summary> 釣り竿のリスト。 </summary>
        public IReadOnlyList<RodParameter> RodParameters => _parameters;
        /// <summary> Commonのカタログとして公開する全釣り竿。 </summary>
        public IReadOnlyList<IRodParameter> Rods => _parameters;

        [SerializeField, Tooltip("釣り竿のリスト")]
        private List<RodParameter> _parameters;
    }
}
