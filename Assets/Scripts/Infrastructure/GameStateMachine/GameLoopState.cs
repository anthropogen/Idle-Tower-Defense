using TowerDefense.EnemyWaves;

namespace TowerDefense.Infrastructure
{
    public class GameLoopState : IRunGameState
    {
        private readonly EnemyWavesController wavesController;
        private readonly IGameFactory gameFactory;
        private readonly IStaticDataService staticDataService;

        public GameLoopState(IGameFactory gameFactory, IStaticDataService staticDataService)
        {
            this.gameFactory = gameFactory;
            this.staticDataService = staticDataService;
            wavesController = new EnemyWavesController(staticDataService.WavesData, gameFactory);
        }

        public void Enter()
        {
            wavesController.SetWave();
        }

        public void Exit()
        {
            gameFactory.ClearEnemies();
        }

        public void Run()
        {
            wavesController?.Run();
        }
    }
}
