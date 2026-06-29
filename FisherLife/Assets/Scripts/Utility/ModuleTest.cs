using Commons;
using UnityEngine;
using VContainer;

namespace Utility
{
    public class ModuleTest : MonoBehaviour
    {
        [Inject]
        public void Inject(IWorldStateMachine worldStateMachine)
        {
            _worldStateMachine = worldStateMachine;
        }
        private void Start()
        {
            _worldStateMachine.ChangeState(_worldStateType);
        }
        [SerializeField] private WorldStateType _worldStateType;
        private IWorldStateMachine _worldStateMachine;
    }
}
