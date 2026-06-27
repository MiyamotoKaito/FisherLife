using Common;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace WorldStateModule
{
    public class WorldStateMachine : MonoBehaviour, IWorldStateMachine
    {
        public static WorldStateMachine Instance;
        [SerializeReference, SubclassSelector]
        private IState[] _states;
        private Dictionary<WorldStateType, IState> _stateDic = new();

        private IState _currentState;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            foreach (var state in _states)
            {
                _stateDic[state.WorldState] = state;
            }
        }
        [Inject]
        public void Init(IInputActionMapStack inputActionMapStack)
        {
            foreach (var (key, value) in _stateDic)
            {
                value.Init(inputActionMapStack);
            }
        }
        public void ChangeState(WorldStateType worldStateType)
        {
            _currentState.Exit();
            _currentState = _stateDic[worldStateType];
            _currentState.Entry();
        }
    }
}
