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

        [Inject]
        private IWorldStateMachine _worldStateMachine;
    }
}
