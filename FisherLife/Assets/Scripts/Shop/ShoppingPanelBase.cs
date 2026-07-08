using UnityEngine;

namespace ShopModule
{
    public class ShoppingPanelBase : MonoBehaviour
    {
        [SerializeField]
        private ItemBase[] _items;
        private int _index = 0;
        public void Begin()
        {
            _index = 0;
            _items[_index].gameObject.SetActive(true);
        }
        public virtual void Up()
        {
            if (_index == 0) return;

            _items[_index].gameObject.SetActive(false);
            _index--;
            _items[_index].gameObject.SetActive(true);
        }
        public virtual void Down()
        {
            if (_index + 1 == _items.Length) return;

            _items[_index].gameObject.SetActive(false);
            _index++;
            _items[_index].gameObject.SetActive(true);
        }
        public virtual void Right()
        {

        }
        public virtual void Left()
        {

        }
        public virtual void Entry()
        {
            _items[_index].Trade();
        }
    }
}
