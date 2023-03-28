using TowerDefense.Entities;

namespace TowerDefense.Infrastructure
{
    public class LoadLevelState : IGameState
    {
        private const string LevelName = "Game";
        private readonly ISceneLoadService sceneLoadService;
        private readonly IGameFactory gameFactory;
        private readonly GameStateMachine stateMachine;
        private readonly PlayerData playerData;

        public LoadLevelState(ISceneLoadService sceneLoadService, IGameFactory gameFactory, GameStateMachine stateMachine, PlayerData playerData)
        {
            this.sceneLoadService = sceneLoadService;
            this.gameFactory = gameFactory;
            this.stateMachine = stateMachine;
            this.playerData = playerData;
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
            gameFactory.CreateTower(playerData);
            gameFactory.CreateUpgradePanel(playerData);
            gameFactory.CreateCounter(playerData);
            stateMachine.Change<GameLoopState>();
        }
    }
}
