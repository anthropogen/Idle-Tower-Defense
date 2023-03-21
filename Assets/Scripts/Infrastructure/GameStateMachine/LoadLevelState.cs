namespace TowerDefense.Infrastructure
{
    public class LoadLevelState : IGameState
    {
        private const string LevelName = "Game";
        private readonly ISceneLoadService sceneLoadService;
        private readonly IGameFactory gameFactory;
        private readonly GameStateMachine stateMachine;

        public LoadLevelState(ISceneLoadService sceneLoadService, IGameFactory gameFactory, GameStateMachine stateMachine)
        {
            this.sceneLoadService = sceneLoadService;
            this.gameFactory = gameFactory;
            this.stateMachine = stateMachine;
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
            gameFactory.CreateTower();
            UnityEngine.Debug.Log($"<color=orange>create level</color> ");
            stateMachine.Change<GameLoopState>();
        }
    }
}
