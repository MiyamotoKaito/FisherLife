using Common;

namespace FishingModule
{
    [System.Serializable]
    public class FishingState : IState
    {
        public WorldStateType WorldState => WorldStateType.Fishing;

        public void Init(IInputActionMapStack inputActionMapStack)
        {
            _inputActionMapStack = inputActionMapStack;
        }

        public void Entry()
        {
            _inputActionMapStack.Push(InputActionMapType.Player);
        }

        public void Exit()
        {
            _inputActionMapStack?.Pop();
        }
        private IInputActionMapStack _inputActionMapStack;
    }
}
