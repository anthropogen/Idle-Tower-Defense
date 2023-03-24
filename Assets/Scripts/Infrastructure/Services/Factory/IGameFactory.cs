using TowerDefense.Entities;

namespace TowerDefense.Infrastructure
{
    public interface IGameFactory : IService
    {
        void ClearEnemies();
        Enemy CreateEnemy(EnemyType type);
        Tower CreateTower();
    }
}