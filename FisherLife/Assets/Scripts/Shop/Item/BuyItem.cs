using Cysharp.Threading.Tasks;
using PlayerModule;
using System.Threading.Tasks;
using UnityEngine;
using Utility;

namespace ShopModule
{
    public class BuyItem : TradeItem
    {
        [SerializeField]
        private RodParameter _rodParameter;

        public override ValueTask<bool> IsTrade()
            => new ValueTask<bool>(_moneyModel.CanSpend(_rodParameter.Price));

        public override async UniTask Trade()
        {
            if (!_moneyModel.CanSpend(_rodParameter.Price)) return;

            var rods = await SaveSystem.LoadAsync<RodCountData>();

            // まだ持っていない竿だけ所持リストへ追加する。
            if (!rods.RodList.Exists(rod => rod.RodName == _rodParameter.Name))
            {
                rods.RodList.Add(new RodData
                {
                    RodName = _rodParameter.Name,
                    RodLevel = _rodParameter.Level,
                    CriticalMultiplier = _rodParameter.CriticalMutiplier,
                    CriticalRate = _rodParameter.CriticalRate,
                    AttackPower = _rodParameter.AttackPower,
                });
            }

            _moneyModel.Add(-_rodParameter.Price);

            await SaveSystem.SaveAsync<RodCountData>();
            await _moneyModel.SaveAsync();
        }

        private void Awake()
        {
            _displayName = _rodParameter.Name;
            _price = _rodParameter.Price;
            _sprite = _rodParameter.Image;
        }
    }
}
