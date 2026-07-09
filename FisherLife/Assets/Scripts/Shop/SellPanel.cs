using Cysharp.Threading.Tasks;
using FishModule;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace ShopModule
{
    /// <summary>
    ///     売るパネル。固定の行ビュー(_rows)に、所持数>0の魚だけを
    ///     _cursor の窓で上から順に割り当てて表示する。表示行数は _rows.Length。
    /// </summary>
    public class SellPanel : ShoppingPanelBase
    {
        [SerializeField, Tooltip("固定の行ビュー(LayoutGroup配下に配置)。この数がそのまま表示行数になる。")]
        private SellItem[] _rows;
        [SerializeField, Tooltip("魚マスタ。")]
        private FishListAsset _fishListAsset;

        // 所持数>0の魚だけを保持する（count==0は選択肢に入れない）。
        private readonly List<FishParameter> _owned = new();

        protected override int ItemCount => _owned.Count;

        // 表示行数＝行ビューの数。
        protected override int MaxVisible => _rows.Length;

        public override void Begin()
        {
            BuildAsync().Forget();
        }

        private async UniTaskVoid BuildAsync()
        {
            var data = await SaveSystem.LoadAsync<FishCountData>();

            _owned.Clear();
            foreach (var param in _fishListAsset.AllFishParameters)
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

        protected override void Render()
        {
            for (int i = 0; i < _rows.Length; i++)
            {
                int dataIndex = _top + i; // 窓の先頭から上→下に割り当て

                if (dataIndex < _owned.Count)
                {
                    _rows[i].gameObject.SetActive(true);
                    _rows[i].Setup(_owned[dataIndex]);          // 行ビューに魚を流し込む
                    _rows[i].SetSelected(dataIndex == _cursor); // 選択行だけ拡大
                }
                else
                {
                    _rows[i].gameObject.SetActive(false);
                }
            }

            ShowImage(_owned.Count > 0 ? _rows[_cursor - _top].Sprite : null);
        }

        public override void Entry(ShoppingController controller)
        {
            if (ItemCount == 0) return;

            // 選択中のデータが乗っている行ビューに委譲する。
            _rows[_cursor - _top].OnEntry(controller);
        }
    }
}
