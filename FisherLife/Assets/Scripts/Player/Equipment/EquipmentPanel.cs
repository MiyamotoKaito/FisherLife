using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace PlayerModule
{
    /// <summary>
    ///     装備切り替えパネル。所持竿を最大_rows.Length行の窓で表示し、
    ///     Entryで選択中の竿を装備する。
    /// </summary>
    public class EquipmentPanel : MonoBehaviour
    {
        [SerializeField, Tooltip("固定の行ビュー(LayoutGroup配下)。この数が表示行数になる。")]
        private RodRowView[] _rows;

        [Header("選択中の竿の詳細表示")]
        [SerializeField, Tooltip("全竿のマスタ(画像/説明の名前引き用)。")]
        private RodListAsset _rodListAsset;
        [SerializeField, Tooltip("選択中の竿の画像。")]
        private Image _detailImage;
        [SerializeField, Tooltip("選択中の竿の説明文。")]
        private TextMeshProUGUI _detailDescription;

        [Inject]
        private RodInventoryModel _inventory;

        private int _cursor; // 選択中の絶対インデックス
        private int _top;    // 表示している窓の先頭インデックス
        private int MaxVisible => _rows.Length;

        public void Begin()
        {
            _cursor = 0;
            _top = 0;
            Render();
        }

        public void Up()
        {
            if (_cursor <= 0) return;

            _cursor--;
            if (_cursor < _top) _top = _cursor;
            Render();
        }

        public void Down()
        {
            if (_cursor >= _inventory.Rods.Count - 1) return;

            _cursor++;
            if (_cursor >= _top + MaxVisible) _top = _cursor - MaxVisible + 1;
            Render();
        }

        public void Entry()
        {
            if (_inventory.Rods.Count == 0) return;

            _inventory.Equip(_inventory.Rods[_cursor]);
            _inventory.SaveAsync().Forget();
            Render(); // 装備マークを更新
        }

        private void Render()
        {
            var rods = _inventory.Rods;
            var equippedName = _inventory.Equipped?.RodName;

            for (int i = 0; i < _rows.Length; i++)
            {
                int idx = _top + i; // 窓の先頭から上→下に割り当て

                if (idx < rods.Count)
                {
                    _rows[i].gameObject.SetActive(true);
                    _rows[i].Bind(rods[idx], rods[idx].RodName == equippedName);
                    _rows[i].SetSelected(idx == _cursor);
                }
                else
                {
                    _rows[i].gameObject.SetActive(false);
                }
            }

            UpdateDetail();
        }

        /// <summary>
        ///     選択中の竿の画像・名前・説明文を表示する（RodParameterを名前引き）。
        /// </summary>
        private void UpdateDetail()
        {
            if (_inventory.Rods.Count == 0) return;

            var rodName = _inventory.Rods[_cursor].RodName;

            // 画像・説明は RodParameter(マスタ)から名前引き。
            var param = FindParam(rodName);
            if (param != null)
            {
                if (_detailImage != null) _detailImage.sprite = param.Image;
                if (_detailDescription != null) _detailDescription.text = param.Description;
            }
        }

        private RodParameter FindParam(string rodName)
        {
            if (_rodListAsset == null || _rodListAsset.RodParameters == null) return null;

            foreach (var rod in _rodListAsset.RodParameters)
            {
                if (rod != null && rod.Name == rodName) return rod;
            }
            return null;
        }
    }
}
