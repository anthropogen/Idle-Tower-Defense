using System;
using System.Collections.Generic;

namespace TowerDefense.Infrastructure
{
    public sealed class GameStateMachine
    {
        private readonly Dictionary<Type, IGameState> _states;
        private IGameState _currentState;

        public GameStateMachine()
        {
            var states = new Dictionary<Type, IGameState>();
            _states = states;
        }

        public void Change<TState>() where TState : class, IGameState
        {
            var type = typeof(TState);
            if (_states.TryGetValue(type, out var next))
            {
                _currentState?.Exit();
                _currentState = next;
                _currentState.Enter();
            }
            else
                throw new InvalidOperationException($"Doesn't have {type} state");
        }
    }
}