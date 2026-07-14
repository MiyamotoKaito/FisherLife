using Cysharp.Threading.Tasks;
using R3;
using Utility;

namespace ShopModule
{
    public class MoneyModel
    {
        public ReadOnlyReactiveProperty<int> Money => _money;

        private readonly ReactiveProperty<int> _money = new(0);

        public async UniTask InitializeAsync()
        {
            var data = await SaveSystem.LoadAsync<MoneyData>();
            _money.Value = data.Money;
        }

        public bool CanSpend(int cost)
        {
            if (_money.Value < cost) return false;
            return true;
        }

        public void Add(int amount) => _money.Value += amount;

        public async UniTask SaveAsync()
        {
            var data = await SaveSystem.LoadAsync<MoneyData>();
            data.Money = _money.Value;
            await SaveSystem.SaveAsync<MoneyData>();
        }
    }
}
