using System;

namespace TowerDefense.Infrastructure
{
    public class BootstrapState : IGameState
    {
        private readonly GameStateMachine stateMachine;
        public BootstrapState(ServiceLocator serviceLocator, Bootstrapper bootstrapper, GameStateMachine stateMachine)
        {
            serviceLocator.Register<ISceneLoadService>(new SceneLoadService(bootstrapper));
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

    public class LoadLevelState : IGameState
    {
        private const string LevelName = "Game";
        private readonly ISceneLoadService sceneLoadService;

        public LoadLevelState(ISceneLoadService sceneLoadService)
        {
            this.sceneLoadService = sceneLoadService;
        }

        public void Enter()
        {
            sceneLoadService.LoadLevel(LevelName, OnLevelLoaded);
        }

        public void Exit()
        {

        }

        private void OnLevelLoaded()
        {
            UnityEngine.Debug.Log("create level");

        }
    }
}
