using UnityEngine;

namespace PlayerModule
{
    public class Door : InteractObjectBase
    {
        public override void Interact()
        {
            Debug.Log("ドアを開ける");
        }
    }
}
