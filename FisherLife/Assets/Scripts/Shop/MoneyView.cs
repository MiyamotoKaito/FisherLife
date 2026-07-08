using Commons;
using DG.Tweening;
using R3;
using TMPro;
using UnityEngine;
using VContainer;

namespace ShopModule
{
    public class MoneyView : MonoBehaviour
    {
        [Inject]
        private IWorldStateMachine _worldStateMachine;
        [SerializeField]
        private Canvas _moneyCanvas;
        [SerializeField]
        private TextMeshProUGUI _text;
        private int _currentMoney;

        public void SetMoney(int money)
        {
            DOVirtual.Int(_currentMoney, money, 1.0f, x =>
            {
                _currentMoney = x;
                _text.text = x.ToString();
            });
        }
        private void Start()
        {
            _worldStateMachine.CurrentStateType.Subscribe(type =>
            {
                if (type == WorldStateType.Moving || type == WorldStateType.Fishing)
                {
                    _moneyCanvas.enabled = true;
                }
                else
                {
                    _moneyCanvas.enabled = false;
                }
            }).RegisterTo(this.destroyCancellationToken);
        }
    }
}
