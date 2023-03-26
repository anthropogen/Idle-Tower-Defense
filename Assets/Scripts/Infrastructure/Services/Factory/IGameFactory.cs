using TowerDefense.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense.Infrastructure
{
    public interface IGameFactory : IService
    {
        Tower Tower { get; }
        void ClearEnemies();
        Enemy CreateEnemy(EnemyType type, Vector2 position);
        Projectile CreateProjectile(Vector2 position, Vector2 direction, float speed, float damage);
        void CreateResultWindow(GameStateMachine stateMachine);
        Tower CreateTower();
    }
}