using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ShopModule
{
    ///<summary>
    ///確認パネルの「はい」行。渡された取引対象を実行して一つ前へ戻る。取引不可時は自身の画像を薄暗くする。
    ///</summary>
    public class ConfirmYesItem : ItemBase
    {
        [SerializeField, Tooltip("行に表示するラベル。")]
        private string _label = "はい";
        [SerializeField, Tooltip("「はい/買う」の画像。取引不可時に薄暗くする。")]
        private Image _image;
        [SerializeField, Tooltip("取引できない時の色(薄暗く)。")]
        private Color _disabledColor = new Color(0.4f, 0.4f, 0.4f, 1f);
        private Color _defaultColor;
        private bool _captured;
        private TradeItem _target;

        ///<summary>
        ///取引対象を渡す（ComfilmPanel.Setupから）。
        ///</summary>
        public void SetTarget(TradeItem target) => _target = target;

        ///<summary>
        ///取引可否に応じて自身の画像の明るさを切り替える。
        ///</summary>
        public void SetTradable(bool canTrade)
        {
            if (_image == null) return;

            if (!_captured)
            {
                _defaultColor = _image.color;
                _captured = true;
            }
            _image.color = canTrade ? _defaultColor : _disabledColor;
        }

        public override async void OnEntry(ShoppingController controller)
        {
            if (_target != null && await _target.IsTrade())
            {
                await _target.Trade();
            }

            controller.Pop();
        }

        private void Awake()
        {
            _displayName = _label;
        }
    }
}
