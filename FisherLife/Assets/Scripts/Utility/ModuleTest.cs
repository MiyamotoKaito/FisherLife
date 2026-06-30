using Commons;
using UnityEngine;
using VContainer;

namespace Utility
{
    /// <summary>
    ///     起動時に指定したワールド状態へ遷移させる動作確認用コンポーネント。
    /// </summary>
    public class ModuleTest : MonoBehaviour
    {
        /// <summary>
        ///     ワールドステートマシンを注入する。
        /// </summary>
        [Inject]
        public void Inject(IWorldStateMachine worldStateMachine)
        {
            _worldStateMachine = worldStateMachine;
        }

        [SerializeField, Tooltip("起動時に遷移するワールド状態。")]
        private WorldStateType _worldStateType;

        private IWorldStateMachine _worldStateMachine;

        /// <summary>
        ///     起動時に指定した状態へ遷移する。
        /// </summary>
        private void Start()
        {
            _worldStateMachine.ChangeState(_worldStateType);
        }
    }
}
