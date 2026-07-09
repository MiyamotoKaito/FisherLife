using Commons;
using Cysharp.Threading.Tasks;
using PlayerModule;
using Unity.Cinemachine;
using UnityEngine;
using VContainer;

namespace Container
{
    public class InGameInitializer : MonoBehaviour
    {
        /// <summary>
        ///     ワールドステートマシンと竿インベントリを注入する。
        /// </summary>
        [Inject]
        public void Inject(IWorldStateMachine worldStateMachine, RodInventoryModel rodInventoryModel)
        {
            _worldStateMachine = worldStateMachine;
            _rodInventoryModel = rodInventoryModel;
        }

        private const int CINEMACHINE_CAMERA_PRIORITY = 0;
        [SerializeField, Tooltip("起動時に遷移するワールド状態。")]
        private WorldStateType _worldStateType;
        [SerializeField, Tooltip("初期化時に使わないシネマティックカメラの配列。")]
        private CinemachineCamera[] _cameras;
        private IWorldStateMachine _worldStateMachine;
        private RodInventoryModel _rodInventoryModel;

        /// <summary>
        ///     竿の装備を確定させてから、指定した状態へ遷移する。
        /// </summary>
        private void Start()
        {
            InitializeAsync().Forget();
        }

        private async UniTaskVoid InitializeAsync()
        {
            await _rodInventoryModel.InitializeAsync();

            _worldStateMachine.ChangeState(_worldStateType);
            foreach (var cam in _cameras)
            {
                cam.Priority = CINEMACHINE_CAMERA_PRIORITY;
                cam.gameObject.SetActive(false);
            }
        }
    }
}
