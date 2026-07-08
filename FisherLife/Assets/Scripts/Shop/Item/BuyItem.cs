using Cysharp.Threading.Tasks;
using PlayerModule;
using System.Threading.Tasks;
using UnityEngine;
using Utility;

namespace ShopModule
{
    public class BuyItem : ItemBase
    {
        [SerializeField]
        private RodParameter _rodParameter;

        public override async ValueTask<bool> IsTrade()
        {
            return _moneyModel.Money.CurrentValue >= _rodParameter.Price;
        }

        public override async UniTask Trade()
        {
            if (!_moneyModel.TrySpend(_rodParameter.Price)) return;

            var rods = await SaveSystem.LoadAsync<RodCountData>();
            if (!rods.RodList.Exists(rod =>
               rod.RodName == _rodParameter.Name
            ))
            {
                var rodData = new RodData()
                {
                    RodName = _rodParameter.Name,
                    RodLevel = _rodParameter.Level,
                    CriticalMultiplier = _rodParameter.CriticalMutiplier,
                    CriticalRate = _rodParameter.CriticalRate,
                    AttackPower = _rodParameter.AttackPower,
                };
                rods.RodList.Add(rodData);
            }

            _moneyModel.Add(-(_rodParameter.Price));
            await _moneyModel.SaveAsync();
        }

        private void Awake()
        {
            _displayName = _rodParameter.Name;
            _price = _rodParameter.Price;
        }
    }
}