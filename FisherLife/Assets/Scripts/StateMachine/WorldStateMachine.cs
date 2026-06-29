using Commons;
using System.Collections.Generic;

namespace StateMachine
{
    public class WorldStateMachine : IWorldStateMachine
    {
        public WorldStateMachine()
        {
            _stateDic = new Dictionary<WorldStateType, IState>();
            _stateStack = new Stack<IState>();
        }
        private IState _currentState;
        private Dictionary<WorldStateType, IState> _stateDic;
        private Stack<IState> _stateStack;
        public void ChangeState(WorldStateType worldStateType)
        {
            if (_stateStack.Count > 0)
            {
                var lastState = _stateStack.Pop();
            }
            _currentState = _stateDic[worldStateType];
            _currentState.Entry();
            _stateStack.Push(_currentState);
        }

        public void BackState()
        {
            if (_stateStack.Count <= 0)
            {
                return;
            }
            _currentState = _stateStack.Pop();

            _currentState.Exit();
        }

        public void AddState(IState state)
        {
            _stateDic.Add(state.WorldState, state);
        }
    }
}