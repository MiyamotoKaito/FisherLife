using UnityEngine;
using UnityEngine.UI;

namespace BattleModule
{
    /// <summary>
    /// 時間制限のView
    /// </summary>
    public class TimeLimitView : MonoBehaviour
    {
        [SerializeField]
        private Image _timeLimitImage;
        /// <summary>
        /// 残り時間を画像のFillAmountに適応する
        /// </summary>
        /// <param name="amount"></param>
        public void UpdateImage(float amount)
        {
            if (_timeLimitImage != null)
            {
                _timeLimitImage.fillAmount = amount;
            }
        }
    }
}
