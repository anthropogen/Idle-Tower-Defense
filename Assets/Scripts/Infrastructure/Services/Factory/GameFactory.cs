using TowerDefense.Entities;
using UnityEngine;

namespace TowerDefense.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private readonly IStaticDataService staticDataService;
        private Tower tower;
        public GameFactory(IStaticDataService staticDataService)
        {
            this.staticDataService = staticDataService;
        }

        public Tower CreateTower()
        {
            tower = GameObject.Instantiate<Tower>(staticDataService.TowerData.Template);
            return tower;
        }

        public Enemy CreateEnemy(EnemyType type)
        {
            var data = staticDataService.GetEnemyDataFor(type);
            var enemy = GameObject.Instantiate<Enemy>(data.Template);
            enemy.Construct(tower, data);
            return enemy;
        }

        public void ClearEnemies()
        {

        }
    }
}