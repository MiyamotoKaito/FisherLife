using Commons;
using UnityEngine;
using VContainer;

namespace PlayerModule
{
    public class FishingArea : InteractObjectBase
    {
        public override void Interact()
        {
            Debug.Log("釣り開始");
            _worldStateMachine.ChangeState(WorldStateType.Fishing);
        }
        private void Awake()
        {
            _mainCamera = Camera.main;
            _interactionCanvas.enabled = false;
        }
        private void Update()
        {
            if (_worldStateMachine.CurrentState != WorldStateType.Moving)
            {
                _interactionCanvas.enabled = false;
            }

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
        [Inject] private IWorldStateMachine _worldStateMachine;
        [SerializeField] private Canvas _interactionCanvas;
        private Camera _mainCamera;
    }
}
