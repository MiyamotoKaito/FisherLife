namespace Commons
{
    public interface IWorldStateMachine
    {
        void AddState(IState state);
        void ChangeState(WorldStateType worldStateType);
        void BackState();
    }
}
