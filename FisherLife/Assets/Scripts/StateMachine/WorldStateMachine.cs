using System.Collections.Generic;
using Commons;

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
        }

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
            // 現在積まれている状態を取り除く。
            if (_stateStack.Count > 0)
            {
                _stateStack.Pop();
            }

            // 新しい状態へ入り、スタックへ積む。
            _currentState = _stateDic[worldStateType];
            _currentState.Entry();
            _stateStack.Push(_currentState);
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

            _currentState = _stateStack.Pop();
            _currentState.Exit();
        }

        private IState _currentState;
        private readonly Dictionary<WorldStateType, IState> _stateDic;
        private readonly Stack<IState> _stateStack;
    }
}
