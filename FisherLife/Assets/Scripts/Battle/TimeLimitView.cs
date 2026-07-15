using UnityEngine;
using UnityEngine.UI;

namespace BattleModule
{
    public class TimeLimitView : MonoBehaviour
    {
        [SerializeField]
        private Image _timeLimitImage;

        public void UpdateImage(float amount)
        {
            if (_timeLimitImage != null)
            {
                _timeLimitImage.fillAmount = amount;
            }
        }
    }
}
