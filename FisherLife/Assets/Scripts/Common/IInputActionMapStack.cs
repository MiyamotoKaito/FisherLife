using UnityEngine.InputSystem;

namespace Commons
{
    public interface IInputActionMapStack
    {
        InputActionMap CurrentMap { get; }
        void Pop();
        void Push(InputActionMapType mapType);
    }
}
