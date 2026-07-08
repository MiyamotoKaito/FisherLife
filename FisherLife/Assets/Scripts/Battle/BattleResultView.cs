using UnityEngine;

namespace BattleModule
{
    public class BattleResultView : MonoBehaviour
    {
        public void SetEnable(bool enable)
        {
            _resultCanvas.enabled = enable;
        }

        [SerializeField]
        private Canvas _resultCanvas;
    }
}
