using TowerDefense.Entities;
using TowerDefense.Infrastructure;
using TowerDefense.StaticData;

namespace TowerDefense.EnemyWaves
{
    public class EnemyWavesController
    {
        private readonly IStaticDataService staticDataService;
        private readonly WaveStaticData wavesData;
        private readonly IGameFactory gameFactory;
        private readonly PlayerData playerData;
        private int waveCount;
        private EnemySpawner spawner;

        public EnemyWavesController(IStaticDataService staticDataService, IGameFactory gameFactory, PlayerData playerData)
        {
            wavesData = staticDataService.WavesData;
            this.staticDataService = staticDataService;
            this.gameFactory = gameFactory;
            this.playerData = playerData;
        }

        public void Run()
        {
            if (spawner == null)
                return;

            if (spawner.IsStopSpawn)
                SetWave();

            spawner?.Run();
        }

        public void SetWave()
        {
            int waveIndex = waveCount >= wavesData.Count ? wavesData.Count - 1 : waveCount;
            var wave = wavesData[waveIndex];
            spawner = new EnemySpawner(gameFactory, wave,staticDataService, playerData);
            waveCount++;
        }
    }
}