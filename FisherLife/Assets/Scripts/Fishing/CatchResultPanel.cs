using Commons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FishingModule
{
    /// <summary>
    ///     釣り上げた魚を知らせる釣果パネル。名前と画像を表示する。
    /// </summary>
    public class CatchResultPanel : MonoBehaviour
    {
        [SerializeField, Tooltip("魚の名前を表示するテキスト。")]
        private TextMeshProUGUI _nameText;
        [SerializeField, Tooltip("魚の画像。")]
        private Image _image;

        /// <summary> 釣り上げた魚を表示する。 </summary>
        public void Show(IFish fish)
        {
            gameObject.SetActive(true);
            if (_nameText != null) _nameText.text =$"<color=red>{fish.Name}</color>をゲットした!!";
            if (_image != null) _image.sprite = fish.Image;
        }

        public void Hide() => gameObject.SetActive(false);
    }
}
