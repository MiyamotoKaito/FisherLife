using Commons;
using System.Collections.Generic;

namespace StateMachine
{
    public class WorldStateMachine : IWorldStateMachine
    {
        private IState _currentState;
        private Dictionary<WorldStateType, IState> _stateDic;
        private Stack<IState> _stateStack;

        public WorldStateMachine(IState[] states)
        {
            Init(states);
        }
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
        private void Init(IState[] states)
        {
            _stateDic = new Dictionary<WorldStateType, IState>();
            _stateStack = new Stack<IState>();
            foreach (var state in states)
            {
                _stateDic[state.WorldState] = state;
            }
        }
    }
}