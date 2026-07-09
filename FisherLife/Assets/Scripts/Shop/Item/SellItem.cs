using Cysharp.Threading.Tasks;
using FishModule;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace ShopModule
{
    public class SellItem : TradeItem
    {
        private FishParameter _fishParameter;

        /// <summary> 表示する魚を設定する（行ビュー再利用のたびに呼ばれる）。 </summary>
        public void Setup(FishParameter param)
        {
            _fishParameter = param;
            _displayName = param.Name;
            _price = param.SellingPrice;
            _sprite = param.Image;
            ApplyDisplayName(); // テキストを更新
        }

        public override async ValueTask<bool> IsTrade()
        {
            var data = await SaveSystem.LoadAsync<FishCountData>();
            return data.Fishes.Exists(fish =>
                fish.FishName == _fishParameter.Name && fish.Count > 0);
        }

        public override async UniTask Trade()
        {
            var fishData = await SaveSystem.LoadAsync<FishCountData>();
            var f = fishData.Fishes.FirstOrDefault(fish => fish.FishName == _fishParameter.Name);

            // 在庫が無ければ売らない。
            if (f == null || f.Count <= 0) return;

            f.Count--;
            _moneyModel.Add(_fishParameter.SellingPrice);

            await SaveSystem.SaveAsync<FishCountData>();
            await _moneyModel.SaveAsync();
        }
    }
}
