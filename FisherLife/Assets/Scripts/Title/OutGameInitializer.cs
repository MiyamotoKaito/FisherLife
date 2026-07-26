using Commons;
using UnityEngine;
using VContainer;

namespace TitleModule
{
    /// <summary>
    /// OutGameの初期化クラス
    /// </summary>
    public class OutGameInitializer : MonoBehaviour
    {
        [Inject]
        public void Inject(IWorldStateMachine worldStateMachine)
        {
            _worldStateMachine = worldStateMachine;
        }

        private IWorldStateMachine _worldStateMachine;

        private void Start()
        {
            _worldStateMachine.ChangeState(WorldStateType.OutGame);
        }
    }
}