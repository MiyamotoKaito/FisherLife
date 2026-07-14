using DG.Tweening;
using TMPro;
using UnityEngine;

namespace ShopModule
{
    ///<summary>
    ///ショップの1行の基底。選択(Entry)された時の振る舞いだけを共通に持つ。
    ///</summary>
    public abstract class ItemBase : MonoBehaviour
    {
        public string DisplayName => _displayName;
        public int Price => _price;
        public Sprite Sprite => _sprite;

        protected string _displayName;
        protected int _price;
        protected Sprite _sprite;

        [SerializeField, Tooltip("DisplayNameを表示するテキスト。")]
        private TextMeshProUGUI _displayText;
        [SerializeField, Tooltip("選択中のスケール倍率。")]
        private float _selectedScale = 1.2f;
        [SerializeField, Tooltip("スケール変化の時間(秒)。")]
        private float _scaleDuration = 0.15f;
        private Vector3 _baseScale;
        private bool _scaleCaptured;

        ///<summary>
        ///この行が選択(Entry)された時の遷移。
        ///</summary>
        public abstract void OnEntry(ShoppingController controller);

        ///<summary>
        ///カーソルが乗っている行だけ少し拡大する。
        ///</summary>
        public void SetSelected(bool selected)
        {
            if (!_scaleCaptured)
            {
                _baseScale = transform.localScale;
                _scaleCaptured = true;
            }

            transform.DOKill();
            transform.DOScale(selected ? _baseScale * _selectedScale : _baseScale, _scaleDuration);
        }

        // 再バインド時にもテキストへ反映できるよう分けている。
        protected void ApplyDisplayName()
        {
            if (_displayText != null)
            {
                _displayText.text = _displayName;
            }
        }

        protected virtual void Start()
        {
            ApplyDisplayName();
        }
    }
}
