using Commons;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Utility
{
    public class WorldStateMachine : MonoBehaviour, IWorldStateMachine
    {
        private static WorldStateMachine _instance;
        [SerializeReference, SubclassSelector]
        private IState[] _states;
        private Dictionary<WorldStateType, IState> _stateDic;

        private IState _currentState;
        private IInputActionMapStack _inputActionMapStack;
        private void Start()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            _stateDic = new Dictionary<WorldStateType, IState>();
            foreach (var state in _states)
            {
                state.Init(_inputActionMapStack);
                _stateDic[state.WorldState] = state;
            }
        }
        [Inject]
        public void Init(IInputActionMapStack inputActionMapStack)
        {
            _inputActionMapStack = inputActionMapStack;
        }
        public void ChangeState(WorldStateType worldStateType)
        {
            if (_currentState != null)
            {
                _currentState.Exit();
            }
            _currentState = _stateDic[worldStateType];
            _currentState.Entry();
        }
    }
}