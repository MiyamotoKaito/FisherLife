using System.Collections.Generic;
using UnityEngine;

namespace FishModule
{
    [CreateAssetMenu(fileName = "FishListAsset", menuName = "Scriptable Objects/FishListAsset")]
    public class FishListAsset : ScriptableObject
    {
        public Dictionary<byte, List<FishParameter>> FishParameters => _fishParameterDictionary;

        public void InitDictionaries()
        {
            _fishParameterDictionary = new();
            foreach (var fishParameter in _fishParameters)
            {
                if (!_fishParameterDictionary.ContainsKey(fishParameter.Level))
                {
                    _fishParameterDictionary[fishParameter.Level] = new();
                }
                _fishParameterDictionary[fishParameter.Level].Add(fishParameter);
            }
        }

        [SerializeField] private List<FishParameter> _fishParameters;
        private Dictionary<byte, List<FishParameter>> _fishParameterDictionary;
    }
}
