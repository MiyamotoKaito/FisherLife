namespace Common
{
    public interface IState
    {
        WorldStateType WorldState { get; }
        void Init(IInputActionMapStack inputActionMapStack);
        void Entry();
        void Exit();
    }
}
