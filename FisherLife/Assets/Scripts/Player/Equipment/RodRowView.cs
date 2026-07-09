using DG.Tweening;
using TMPro;
using UnityEngine;
using Utility;

namespace PlayerModule
{
    /// <summary>
    ///     装備リストの1行ビュー。竿名を表示し、選択中は拡大、装備中はマーク表示。
    /// </summary>
    public class RodRowView : MonoBehaviour
    {
        [SerializeField, Tooltip("竿名を表示するテキスト。")]
        private TextMeshProUGUI _nameText;
        [SerializeField, Tooltip("装備中に表示するマーク。")]
        private GameObject _equippedMark;
        [SerializeField, Tooltip("選択中のスケール倍率。")]
        private float _selectedScale = 1.2f;
        [SerializeField, Tooltip("スケール変化の時間(秒)。")]
        private float _scaleDuration = 0.15f;

        private Vector3 _baseScale;
        private bool _captured;

        /// <summary> 表示する竿を割り当てる。 </summary>
        public void Bind(RodData rod, bool equipped)
        {
            if (_nameText != null) _nameText.text = rod.RodName;
            if (_equippedMark != null) _equippedMark.SetActive(equipped);
        }

        /// <summary> カーソルが乗っている行だけ少し拡大する。 </summary>
        public void SetSelected(bool selected)
        {
            if (!_captured)
            {
                _baseScale = transform.localScale;
                _captured = true;
            }
            transform.DOKill();
            transform.DOScale(selected ? _baseScale * _selectedScale : _baseScale, _scaleDuration);
        }
    }
}
