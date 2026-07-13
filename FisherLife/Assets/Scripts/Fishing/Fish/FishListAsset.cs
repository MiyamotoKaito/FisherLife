using System.Collections.Generic;
using Commons;
using UnityEngine;

namespace FishModule
{
    [CreateAssetMenu(fileName = "FishListAsset", menuName = "Scriptable Objects/FishListAsset")]
    public class FishListAsset : ScriptableObject, IFishCatalog
    {
        public Dictionary<byte, List<FishParameter>> FishParameters => _fishParameterDictionary;
        /// <summary> 登録されている全魚種（InitDictionaries不要で読める）。 </summary>
        public IReadOnlyList<FishParameter> AllFishParameters => _fishParameters;
        /// <summary> Commonのカタログとして公開する全魚種。 </summary>
        public IReadOnlyList<IFishParameter> Fishes => _fishParameters;
        /// <summary>
        /// 辞書の初期化
        /// </summary>
        public void InitDictionaries()
        {
            _fishParameterDictionary = new();
            _fishParameterByName = new();
            foreach (var fishParameter in _fishParameters)
            {
                if (!_fishParameterDictionary.ContainsKey(fishParameter.Level))
                {
                    _fishParameterDictionary[fishParameter.Level] = new();
                }
                _fishParameterDictionary[fishParameter.Level].Add(fishParameter);
                _fishParameterByName[fishParameter.Name] = fishParameter;
            }
        }
        /// <summary>
        /// 魚名から売値を引く。見つからなければ false。
        /// </summary>
        public bool TryGetSellingPrice(string name, out int sellingPrice)
        {
            if (_fishParameterByName != null &&
                _fishParameterByName.TryGetValue(name, out var p))
            {
                sellingPrice = p.SellingPrice;
                return true;
            }
            sellingPrice = 0;
            return false;
        }
        [SerializeField] private List<FishParameter> _fishParameters;
        private Dictionary<byte, List<FishParameter>> _fishParameterDictionary;
        private Dictionary<string, FishParameter> _fishParameterByName;
    }
}
