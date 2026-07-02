using UnityEngine;

namespace FishModule
{
    /// <summary>
    ///     魚の見た目を扱うView。
    /// </summary>
    public class FishView : MonoBehaviour
    {
        public FishModel FishModel => _fishModel;
        public void Init(FishParameter fishParameter)
        {
            _fishModel = new FishModel(fishParameter);
        }

        private FishModel _fishModel;
    }
}
