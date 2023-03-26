using NTC.Global.System;
using System;
using TowerDefense.EnemyWaves;
using TowerDefense.Entities;

namespace TowerDefense.Infrastructure
{
    public class GameLoopState : IRunGameState
    {
        private readonly EnemyWavesController wavesController;
        private readonly IGameFactory gameFactory;
        private readonly GameStateMachine stateMachine;

        public GameLoopState(IGameFactory gameFactory, GameStateMachine stateMachine, IStaticDataService staticDataService)
        {
            this.gameFactory = gameFactory;
            this.stateMachine = stateMachine;
            wavesController = new EnemyWavesController(staticDataService.WavesData, gameFactory);
        }

        public void Enter()
        {
            wavesController.SetWave();
            gameFactory.Tower.Get<Damageable>().Died += OnTowerDestroyed;
        }

        public void Exit()
        {
            gameFactory.ClearEnemies();
            gameFactory.Tower.Get<Damageable>().Died -= OnTowerDestroyed;
        }

        public void Run()
        {
            wavesController?.Run();
        }


        private void OnTowerDestroyed()
        {
            gameFactory.Tower.Disable();
            stateMachine.Change<FinishState>();
        }
    }
}
