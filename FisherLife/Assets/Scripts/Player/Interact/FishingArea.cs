using UnityEngine;

namespace PlayerModule
{
    public class FishingArea : MonoBehaviour
    {
        void Start()
        {
            if (!TryGetComponent<Collider>(out var collider))
            {
                Debug.LogError($"このFishingAreaにはColliderがありません{name}");
                return;
            }
        }
    }
}
