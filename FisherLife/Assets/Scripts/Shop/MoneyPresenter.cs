using Cysharp.Threading.Tasks;
using R3;
using System.Threading;
using UnityEngine;
using VContainer;

namespace ShopModule
{
    public class MoneyPresenter : MonoBehaviour
    {
        private MoneyModel _moneyModel;
        private MoneyView _moneyView;
        private CancellationTokenSource _cancellationTokenSource;

        [Inject]
        public void Inject(MoneyModel model, MoneyView view)
        {
            _moneyModel = model;
            _moneyView = view;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        private async void Start()
        {
            await SubscribeAsync();
        }

        private async UniTask SubscribeAsync()
        {
            Debug.Log("金を設定");
            await _moneyModel.InitializeAsync();

            _moneyModel.Money.Subscribe(money =>
            {
                _moneyView.SetMoney(money);
            }).RegisterTo(_cancellationTokenSource.Token);
        }
    }
}
