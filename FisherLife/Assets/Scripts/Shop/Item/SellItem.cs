using Cysharp.Threading.Tasks;
using FishModule;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Utility;

namespace ShopModule
{
    public class SellItem : ItemBase
    {
        [SerializeField]
        private FishParameter _fishParameter;
        private bool _isHaveFish;
        public override async ValueTask<bool> IsTrade()
        {
            IsHaveFish();

            return _isHaveFish;
        }
        private async void IsHaveFish()
        {
            var data = await SaveSystem.LoadAsync<FishCountData>();

            _isHaveFish = data.Fishes.Exists(fish =>
                 fish.FishName == _fishParameter.Name && fish.Count > 0);
        }
        public override async UniTask Trade()
        {
            if (await IsTrade())
            {
                _moneyModel.Add(_fishParameter.SellingPrice);

                var fishData = await SaveSystem.LoadAsync<FishCountData>();

                var f = fishData.Fishes.FirstOrDefault(fish => fish.FishName == _fishParameter.Name);
                f.Count--;
            }
        }

        private void Awake()
        {
            _displayName = _fishParameter.Name;
            _price = _fishParameter.SellingPrice;
        }
    }
}
