﻿using System;
using System.Collections.Generic;

namespace TowerDefense.Infrastructure
{
    public sealed class GameStateMachine
    {
        private readonly Dictionary<Type, IGameState> states;
        private IGameState currentState;
        private IRunGameState runState;

        public GameStateMachine(ServiceLocator serviceLocator, Bootstrapper bootstrapper)
        {
            states = new Dictionary<Type, IGameState>();
            states[typeof(BootstrapState)] = new BootstrapState(serviceLocator, bootstrapper, this);
            states[typeof(LoadLevelState)] = new LoadLevelState(serviceLocator.Release<ISceneLoadService>(), serviceLocator.Release<IGameFactory>(), this);
            states[typeof(GameLoopState)] = new GameLoopState(serviceLocator.Release<IGameFactory>(), this, serviceLocator.Release<IStaticDataService>());
            states[typeof(FinishState)] = new FinishState(serviceLocator.Release<IGameFactory>(), this);
        }

        public void Change<TState>() where TState : class, IGameState
        {
            var type = typeof(TState);
            if (states.TryGetValue(type, out var next))
            {
                currentState?.Exit();
                runState = next as IRunGameState;
                currentState = next;
                currentState.Enter();
            }
            else
                throw new InvalidOperationException($"Doesn't have {type} state");
        }

        public void Run()
            => runState?.Run();
    }
}