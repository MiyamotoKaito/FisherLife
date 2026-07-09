using Commons;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utility;
using VContainer;

namespace PlayerModule
{
    public class Door : InteractObjectBase
    {
        /// <summary>
        /// ドアを開ける処理を行う
        /// ここでShopに遷移する処理を追加する
        /// </summary>
        public override void Interact()
        {
            Debug.Log("ドアを開ける");
            _worldStateMachine.AllStop();

            SceneTransitionManager.IrisIn(_sceneName).Forget();
        }
        private void Awake()
        {
            _mainCamera = Camera.main;
            _interactionCanvas.enabled = false;
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
        [Inject] private IWorldStateMachine _worldStateMachine;
        [SerializeField] private Canvas _interactionCanvas;
        [SerializeField] private string _sceneName;
        private Camera _mainCamera;
    }
}
