using TowerDefense.Entities;
using UnityEngine;

namespace TowerDefense.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private readonly IStaticDataService staticDataService;

        public GameFactory(IStaticDataService staticDataService)
        {
            this.staticDataService = staticDataService;
        }

        public Tower CreateTower()
        {
            return GameObject.Instantiate<Tower>(staticDataService.TowerData.Template);
        }

        public Enemy CreateEnemy(EnemyType type)
        {
            var data = staticDataService.GetEnemyDataFor(type);
            return GameObject.Instantiate<Enemy>(data.Template);
        }
    }
}