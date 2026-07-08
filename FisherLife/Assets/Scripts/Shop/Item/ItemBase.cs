using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace ShopModule
{
    public abstract class ItemBase : MonoBehaviour
    {
        public string DisplayName => _displayName;

        public int Price => _price;
        protected string _displayName;
        protected int _price;
        [Inject]
        protected MoneyModel _moneyModel;
        public abstract ValueTask<bool> IsTrade();
        public abstract UniTask Trade();
    }
}
