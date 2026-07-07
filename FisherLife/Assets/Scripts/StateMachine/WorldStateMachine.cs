using Commons;
using R3;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    /// <summary>
    ///     ワールド状態を管理するステートマシン。
    /// </summary>
    public class WorldStateMachine : IWorldStateMachine
    {
        /// <summary>
        ///     状態の辞書とスタックを初期化する。
        /// </summary>
        public WorldStateMachine()
        {
            _stateDic = new Dictionary<WorldStateType, IState>();
            _stateStack = new Stack<IState>();

            _currentStateType = new();
        }
        public ReactiveProperty<WorldStateType> CurrentStateType => _currentStateType;
        /// <summary>
        ///     状態を登録する。
        /// </summary>
        public void AddState(IState state)
        {
            _stateDic.Add(state.WorldState, state);
        }

        /// <summary>
        ///     指定した状態へ遷移する。
        /// </summary>
        public void ChangeState(WorldStateType worldStateType)
        {
            if (_stateStack.Count > 0)
            {
                _currentState.Exit();
            }
            // 新しい状態へ入り、スタックへ積む。
            _currentState = _stateDic[worldStateType];
            _currentStateType.Value = worldStateType;
            _currentState.Entry();
            _stateStack.Push(_currentState);
            Debug.Log($"ステートを変えました。{worldStateType}");
        }

        /// <summary>
        ///     直前の状態へ戻る。
        /// </summary>
        public void BackState()
        {
            if (_stateStack.Count <= 0)
            {
                return;
            }
            _currentState?.Exit();
            _stateStack.Pop();
            _currentState = _stateStack.Count > 0 ? _stateStack.Peek() : null;
            _currentState?.Entry();
            _currentStateType.Value = _currentState.WorldState;
            Debug.Log($"以前のステートに戻りました。{_currentState?.WorldState}");
        }
        public void AllStop()
        {
            _currentState?.Exit();
            _stateStack.Clear();
            _currentState = null;
            Debug.Log("全てのステートを停止しました。");
        }

        public void Dispose()
        {
            if (_currentStateType != null)
            {
                _currentStateType = null;
            }
        }

        private ReactiveProperty<WorldStateType> _currentStateType;
        private IState _currentState;
        private readonly Dictionary<WorldStateType, IState> _stateDic;
        private readonly Stack<IState> _stateStack;
    }
}
