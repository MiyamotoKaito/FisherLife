namespace Commons
{
    public interface IWorldStateMachine
    {
        void ChangeState(WorldStateType worldStateType);
        void BackState();
    }
}
