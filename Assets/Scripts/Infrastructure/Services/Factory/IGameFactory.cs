using TowerDefense.Entities;

namespace TowerDefense.Infrastructure
{
    public interface IGameFactory : IService
    {
        Enemy CreateEnemy(EnemyType type);
        Tower CreateTower();
    }
}