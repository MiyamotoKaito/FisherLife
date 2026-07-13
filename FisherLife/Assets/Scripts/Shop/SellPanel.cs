using Commons;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using VContainer;

namespace ShopModule
{
    ///<summary>
    ///売るパネル。所持数>0の魚だけを窓表示し、Entryで確認へ進む。
    ///</summary>
    public class SellPanel : ShoppingPanelBase
    {
        protected override int ItemCount => _owned.Count;
        protected override int MaxVisible => _rows.Length;

        [SerializeField, Tooltip("固定の行ビュー(LayoutGroup配下)。この数がそのまま表示行数になる。")]
        private SellItem[] _rows;

        [Inject]
        private IFishCatalog _fishCatalog;

        private readonly List<IFishParameter> _owned = new();

        public override void Begin()
        {
            BuildAsync().Forget();
        }

        public override void Entry(ShoppingController controller)
        {
            if (ItemCount == 0) return;

            _rows[_cursor - _top].OnEntry(controller);
        }

        protected override void Render()
        {
            for (int i = 0; i < _rows.Length; i++)
            {
                int dataIndex = _top + i;

                if (dataIndex < _owned.Count)
                {
                    _rows[i].gameObject.SetActive(true);
                    _rows[i].Setup(_owned[dataIndex]);
                    _rows[i].SetSelected(dataIndex == _cursor);
                }
                else
                {
                    _rows[i].gameObject.SetActive(false);
                }
            }

            ShowImage(_owned.Count > 0 ? _rows[_cursor - _top].Sprite : null);
        }

        // 所持数>0の魚だけをカタログから抽出して窓表示する。
        private async UniTaskVoid BuildAsync()
        {
            var data = await SaveSystem.LoadAsync<FishCountData>();

            _owned.Clear();
            foreach (var param in _fishCatalog.Fishes)
            {
                var entry = data.Fishes.Find(f => f.FishName == param.Name);
                if (entry != null && entry.Count > 0)
                {
                    _owned.Add(param);
                }
            }

            _cursor = 0;
            _top = 0;
            Render();
        }
    }
}
