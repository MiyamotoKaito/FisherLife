using Commons;
using UnityEngine;
using VContainer;

namespace PlayerModule
{
    public class Clerk : InteractObjectBase
    {
        public override void Interact()
        {
            _worldStateMachine.ChangeState(WorldStateType.Shopping);
        }
        private void Awake()
        {
            _mainCamera = Camera.main;
        }
        private void Update()
        {
            if (_interactionCanvas.enabled)
            {
                _interactionCanvas.transform.LookAt(_mainCamera.transform);
                _interactionCanvas.transform.Rotate(0, 180, 0);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _interactionCanvas.enabled = true; 
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _interactionCanvas.enabled = false;
            }
        }
        [Inject]
        private IWorldStateMachine _worldStateMachine;
        [SerializeField] 
        private Canvas _interactionCanvas;
        private Camera _mainCamera;
    }
}
