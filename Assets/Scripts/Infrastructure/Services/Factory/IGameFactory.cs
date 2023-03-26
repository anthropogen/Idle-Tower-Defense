using TowerDefense.Entities;
using UnityEngine;

namespace TowerDefense.Infrastructure
{
    public interface IGameFactory : IService
    {
        void ClearEnemies();
        Enemy CreateEnemy(EnemyType type, Vector2 position);
        Projectile CreateProjectile(Vector2 position, Vector2 direction, float speed, float damage);
        Tower CreateTower();
    }
}