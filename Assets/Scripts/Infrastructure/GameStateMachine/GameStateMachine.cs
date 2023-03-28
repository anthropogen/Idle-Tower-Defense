using System;
using System.Collections.Generic;
using TowerDefense.Entities;

namespace TowerDefense.Infrastructure
{
    public sealed class GameStateMachine
    {
        private readonly Dictionary<Type, IGameState> states;
        private IGameState currentState;
        private IRunGameState runState;

        public GameStateMachine(ServiceLocator serviceLocator, Bootstrapper bootstrapper)
        {
            PlayerData playerData = new PlayerData();
            states = new Dictionary<Type, IGameState>();
            states[typeof(BootstrapState)] = new BootstrapState(serviceLocator, bootstrapper, this);
            states[typeof(LoadLevelState)] = new LoadLevelState(serviceLocator.Release<ISceneLoadService>(), serviceLocator.Release<IGameFactory>(), this,playerData);
            states[typeof(GameLoopState)] = new GameLoopState(serviceLocator.Release<IGameFactory>(), this, serviceLocator.Release<IStaticDataService>(),playerData);
            states[typeof(FinishState)] = new FinishState(serviceLocator.Release<IGameFactory>(), this,playerData);
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
            UnityEngine.Debug.Log($"<color=orange>transit to {type}</color> ");
        }

        public void Run()
            => runState?.Run();
    }
}