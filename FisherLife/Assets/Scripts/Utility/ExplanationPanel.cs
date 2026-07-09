using Commons;
using R3;
using UnityEngine;
using VContainer;

namespace Utility
{
    public class ExplanationPanel : MonoBehaviour
    {
        [Inject]
        private IWorldStateMachine _stateMachine;
        [SerializeField]
        private WorldStateType _stateType;
        [SerializeField]
        private Canvas _ui;
        private void Start()
        {
            _stateMachine.CurrentStateType.Subscribe(state =>
            {
                if (state == _stateType)
                {
                    _ui.enabled = true;
                }
                else
                {
                    _ui.enabled = false;
                }
            }).RegisterTo(destroyCancellationToken);
        }
    }
}
