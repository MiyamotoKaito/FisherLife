using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace ShopModule
{
    /// <summary>
    ///     取引の確認パネル。表示・カーソルは基底に任せ、対象のセットとメッセージ更新を担う。
    ///     取引できない(お金不足/在庫なし)場合は「はい」の画像を薄暗くし、「はい」へ移動できないようにする。
    ///     _items は「はい(ConfirmYesItem)=先頭」「いいえ(BackItem)」の順で並べる。
    /// </summary>
    public class ComfilmPanel : ShoppingPanelBase
    {
        [SerializeField, Tooltip("「〇〇を取引しますか?」等の表示。")]
        private TextMeshProUGUI _messageText;
        [SerializeField, Tooltip("「はい」行。取引対象を渡し、取引不可時に薄暗くする。")]
        private ConfirmYesItem _yesItem;

        private TradeItem _item;
        private bool _canTrade;

        /// <summary> 取引対象をセットする（ShoppingController.PushConfirmから呼ばれる）。 </summary>
        public void Setup(TradeItem item)
        {
            _item = item;
            _yesItem.SetTarget(item);
        }

        public override void Begin()
        {
            BeginAsync().Forget();
        }

        private async UniTaskVoid BeginAsync()
        {
            _canTrade = _item != null && await _item.IsTrade();

            // 「はい」の画像を薄暗く/通常に切り替える。
            _yesItem.SetTradable(_canTrade);

            // 買えない時は「はい」(先頭)へ行けないよう、カーソルを「いいえ」に寄せる。
            _cursor = _canTrade ? 0 : 1;
            _top = 0;
            Render();

            if (_messageText != null && _item != null)
            {
                _messageText.text = $"{_item.DisplayName}  ({_item.Price})";
            }
        }

        // 取引できない時は「はい」(index 0)へ上がれない。
        public override void Up()
        {
            if (!_canTrade) return;
            base.Up();
        }
    }
}
