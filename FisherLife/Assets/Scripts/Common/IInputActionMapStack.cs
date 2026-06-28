using UnityEngine.InputSystem;

namespace Common
{
    public interface IInputActionMapStack
    {
        InputActionMap CurrentMap { get; }
        void Pop();
        void Push(InputActionMapType mapType);
    }
}
