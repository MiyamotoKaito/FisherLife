using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace PlayerModule
{
    public class PlayerView : MonoBehaviour
    {

        private InputActionAsset _actionAsset;
        [Inject]
        public void Inject(InputActionAsset inputActions)
        {
            _actionAsset = inputActions;
        }
        private void Start()
        {
           
        }
    }
}
