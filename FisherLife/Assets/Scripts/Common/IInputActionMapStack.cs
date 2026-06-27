namespace Common
{
    public interface IInputActionMapStack
    {
        void Pop();
        void Push(InputActionMapType mapType);
    }
}
