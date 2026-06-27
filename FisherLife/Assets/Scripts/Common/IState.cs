namespace Common
{
    public interface IState
    {
        WorldStateType WorldState { get; }
        void Entry();
        void Exit(WorldStateType worldState);
    }
}
