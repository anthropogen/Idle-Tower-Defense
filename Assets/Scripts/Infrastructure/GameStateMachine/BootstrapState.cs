﻿using UnityEngine;

namespace TowerDefense.Infrastructure
{
    public class BootstrapState : IGameState
    {
        private readonly GameStateMachine stateMachine;
        public BootstrapState(ServiceLocator serviceLocator, Bootstrapper bootstrapper, GameStateMachine stateMachine)
        {
            serviceLocator.Register<IAssetProvider>(new AssetProvider());
            serviceLocator.Register<ISceneLoadService>(new SceneLoadService(bootstrapper));
            serviceLocator.Register<IStaticDataService>(new StaticDataService());
            serviceLocator.Register<IGameFactory>(new GameFactory(serviceLocator.Release<IStaticDataService>(),serviceLocator.Release<IAssetProvider>()));
            this.stateMachine = stateMachine;
        }

        public void Enter()
        {
            stateMachine.Change<LoadLevelState>();
        }

        public void Exit()
        {

        }
    }
}
