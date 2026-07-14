using UnityEngine;
using UnityEngine.UI;

namespace ShopModule
{
    ///<summary>
    ///ショップの1画面。ItemBaseの配列を「最大MaxVisible行の窓」で表示し、カーソルが窓の外へ出たらスクロールする。
    ///データ駆動の派生(SellPanel等)は ItemCount / MaxVisible / Render / Entry を override する。
    ///</summary>
    public class ShoppingPanelBase : MonoBehaviour
    {
        protected virtual int ItemCount => _items.Length;
        protected virtual int MaxVisible => _maxVisible;

        [SerializeField, Tooltip("画像を表示するためのUI。")]
        protected Image _image;
        protected int _cursor;
        protected int _top;

        [SerializeField]
        private ItemBase[] _items;
        [SerializeField, Tooltip("同時に表示する最大行数。")]
        private int _maxVisible = 6;

        public virtual void Begin()
        {
            _cursor = 0;
            _top = 0;
            Render();
        }

        public virtual void Up()
        {
            if (_cursor <= 0) return;

            _cursor--;
            // カーソルが窓の上に出たら上へスクロール。
            if (_cursor < _top) _top = _cursor;
            Render();
        }

        public virtual void Down()
        {
            if (_cursor >= ItemCount - 1) return;

            _cursor++;
            // カーソルが窓の下に出たら下へスクロール。
            if (_cursor >= _top + MaxVisible) _top = _cursor - MaxVisible + 1;
            Render();
        }

        public virtual void Right()
        {
        }

        public virtual void Left()
        {
        }

        public virtual void Entry(ShoppingController controller)
        {
            if (ItemCount == 0) return;

            // 遷移も取引も選択中の行の多態に委譲する。
            _items[_cursor].OnEntry(controller);
        }

        protected virtual void Render()
        {
            for (int i = 0; i < _items.Length; i++)
            {
                bool visible = i >= _top && i < _top + MaxVisible;
                _items[i].gameObject.SetActive(visible);
                _items[i].SetSelected(i == _cursor);
            }

            ShowImage(ItemCount > 0 ? _items[_cursor].Sprite : null);
        }

        protected void ShowImage(Sprite sprite)
        {
            if (_image == null) return;

            _image.sprite = sprite;
            _image.enabled = sprite != null;
        }
    }
}
