using UnityEngine;

namespace ShopModule
{
    ///<summary>
    ///選択すると次のパネルをスタックに積む行(買う/売る の分岐など)。取引はしないので IsTrade/Trade は持たない。
    ///</summary>
    public class NextPanelItem : ItemBase
    {
        [SerializeField, Tooltip("行に表示するラベル。")]
        private string _label;
        [SerializeField, Tooltip("Entryで開くパネル。")]
        private ShoppingPanelBase _nextPanel;

        public override void OnEntry(ShoppingController controller)
            => controller.Push(_nextPanel);

        private void Awake()
        {
            _displayName = _label;
        }
    }
}
