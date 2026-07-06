using Commons;
using VContainer;

namespace PlayerModule
{
    public class FishingArea : InteractObjectBase
    {
        public override void Interact()
        {
            _worldStateMachine.ChangeState(WorldStateType.Fishing);
        }

        [Inject] private IWorldStateMachine _worldStateMachine;
    }
}
