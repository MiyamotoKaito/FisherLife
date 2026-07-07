using Commons;
using R3;
using System.Threading;
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
            _cancellationTokenSource = new CancellationTokenSource();
            _worldStateMachine.CurrentStateType.Subscribe(state =>
            {
                if (state == WorldStateType.Moving)
                {
                    _enabled = true;
                }
                else
                {
                    _enabled = false;
                }
            }).RegisterTo(_cancellationTokenSource.Token);
        }
        private void Update()
        {
            if (!_enabled)
                return;
            _interactionCanvas.transform.LookAt(_mainCamera.transform);
            _interactionCanvas.transform.Rotate(0, 180, 0);
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
        private void OnDestroy()
        {
            if (_mainCamera)
                _mainCamera = null;
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
            }
        }
        [Inject] private IWorldStateMachine _worldStateMachine;
        [SerializeField] private Canvas _interactionCanvas;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _enabled = false;
        private Camera _mainCamera;
    }
}
