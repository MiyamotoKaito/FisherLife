using Commons;
using UnityEngine;

namespace PlayerModule
{
    public class InteractObjectBase : MonoBehaviour, IInteractable
    {
        public virtual void Interact()
        {
            throw new System.NotImplementedException("このオブジェクトはインタラクションに対応していません");
        }   

        void Start()
        {
            if (!TryGetComponent<Collider>(out var collider))
            {
                Debug.LogError($"このObjectにはColliderが必要です : {name}");
                return;
            }
        }
    }
}
