using Commons;
using Unity.Cinemachine;
using UnityEngine;
using VContainer;

namespace Container
{
    public class InGameInitializer : MonoBehaviour
    {
        /// <summary>
        ///     ワールドステートマシンを注入する。
        /// </summary>
        [Inject]
        public void Inject(IWorldStateMachine worldStateMachine)
        {
            _worldStateMachine = worldStateMachine;
        }

        private const int CINEMACHINE_CAMERA_PRIORITY = 0;
        [SerializeField, Tooltip("起動時に遷移するワールド状態。")]
        private WorldStateType _worldStateType;
        [SerializeField, Tooltip("初期化時に使わないシネマティックカメラの配列。")]
        private CinemachineCamera[] _cameras; 
        private IWorldStateMachine _worldStateMachine;

        /// <summary>
        ///     起動時に指定した状態へ遷移する。
        /// </summary>
        private void Start()
        {
            _worldStateMachine.ChangeState(_worldStateType);
            foreach (var cam in _cameras)
            {
                cam.Priority = CINEMACHINE_CAMERA_PRIORITY;
                cam.gameObject.SetActive(false);
            }
        }
    }
}
