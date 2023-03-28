using TowerDefense.Entities;
using TowerDefense.UI;
using UnityEngine;
using static TowerDefense.Entities.Tower;

namespace TowerDefense.Infrastructure
{
    public interface IGameFactory : IService
    {
        Tower Tower { get; }
        void ClearEnemies();
        Enemy CreateEnemy(EnemyType type, Vector2 position);
        Projectile CreateProjectile(Vector2 position, Vector2 direction, float speed, float damage);
        void CreateResultWindow(GameStateMachine stateMachine);
        UpgradePanel CreateUpgradePanel(PlayerData playerData);
        Tower CreateTower(PlayerData playerData);
        CoinCounter CreateCounter(PlayerData playerData);
    }
}