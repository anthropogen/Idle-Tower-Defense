using TowerDefense.Infrastructure;
using TowerDefense.StaticData;

namespace TowerDefense.EnemyWaves
{
    public class EnemyWavesController
    {
        private readonly WaveStaticData wavesData;
        private readonly IGameFactory gameFactory;
        private int waveCount;
        private EnemySpawner spawner;

        public EnemyWavesController(WaveStaticData wavesData, IGameFactory gameFactory)
        {
            this.wavesData = wavesData;
            this.gameFactory = gameFactory;
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
            spawner = new EnemySpawner(gameFactory, wave);
            waveCount++;
        }
    }
}