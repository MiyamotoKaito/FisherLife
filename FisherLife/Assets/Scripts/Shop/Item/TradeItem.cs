using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using VContainer;

namespace ShopModule
{
    /// <summary>
    ///     買う/売る の取引を行う行の基底。
    ///     Entryされたら確認パネルへ進み、確認側から IsTrade/Trade が呼ばれる。
    /// </summary>
    public abstract class TradeItem : ItemBase
    {
        [Inject]
        protected MoneyModel _moneyModel;

        /// <summary> 取引可能か（お金・在庫の判定）。副作用は持たせない。 </summary>
        public abstract ValueTask<bool> IsTrade();

        /// <summary> 取引を実行して保存する。 </summary>
        public abstract UniTask Trade();

        public override void OnEntry(ShoppingController controller)
            => controller.PushConfirm(this);
    }
}
