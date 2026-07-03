using UnityEngine;

namespace FishModule
{
    /// <summary>
    ///     魚の見た目を扱うView。
    /// </summary>
    public class FishView : MonoBehaviour
    {
        public void SetEnable(bool enable)
        {
            gameObject.SetActive(enable);
        }
        public void SetStartPosition(Vector3 pos)
        {
            transform.position = pos;
        }
    }
}
