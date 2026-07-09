using UnityEngine;

namespace ShopModule
{
    /// <summary>
    ///     選択(Entry)すると一つ前のパネルへ戻る行。
    ///     ルートの選択パネルに置いた場合はそのまま退店になる。
    /// </summary>
    public class BackItem : ItemBase
    {
        [SerializeField, Tooltip("行に表示するラベル。")]
        private string _label = "もどる";

        public override void OnEntry(ShoppingController controller)
            => controller.Pop();

        private void Awake()
        {
            _displayName = _label;
        }
    }
}
