using TowerDefense.Entities;
using TowerDefense.StaticData;

namespace TowerDefense.Infrastructure
{
    public interface IStaticDataService : IService
    {
        TowerStaticData TowerData { get; }
        EnemyStaticData GetEnemyDataFor(EnemyType type);
    }
}