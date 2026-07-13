using Commons;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;
using Utility;
using VContainer;

namespace ShopModule
{
    public class BuyItem : TradeItem
    {
        [SerializeField, Tooltip("買える竿の名前(カタログから引く)。")]
        private string _rodName;

        [Inject]
        private IRodCatalog _rodCatalog;

        private IRodParameter _rod;

        public override ValueTask<bool> IsTrade()
            => new ValueTask<bool>(_rod != null && _moneyModel.CanSpend(_rod.Price));

        public override async UniTask Trade()
        {
            if (_rod == null || !_moneyModel.CanSpend(_rod.Price)) return;

            var rods = await SaveSystem.LoadAsync<RodCountData>();

            // まだ持っていない竿だけ所持リストへ追加する。
            if (!rods.RodList.Exists(rod => rod.RodName == _rod.Name))
            {
                rods.RodList.Add(new RodData
                {
                    RodName = _rod.Name,
                    RodLevel = _rod.Level,
                    AttackPower = _rod.AttackPower,
                    CriticalMultiplier = _rod.CriticalMutiplier,
                    CriticalRate = _rod.CriticalRate,
                });
            }

            _moneyModel.Add(-_rod.Price);

            await SaveSystem.SaveAsync<RodCountData>();
            await _moneyModel.SaveAsync();
        }

        protected override void Start()
        {
            _rod = FindRod(_rodName);
            if (_rod != null)
            {
                _displayName = _rod.Name;
                _price = _rod.Price;
                _sprite = _rod.Image;
            }

            base.Start();
        }

        private IRodParameter FindRod(string rodName)
        {
            foreach (var rod in _rodCatalog.Rods)
            {
                if (rod != null && rod.Name == rodName) return rod;
            }
            return null;
        }
    }
}
