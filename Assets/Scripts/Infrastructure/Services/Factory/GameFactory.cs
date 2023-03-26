using TowerDefense.Entities;
using UnityEngine;

namespace TowerDefense.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private readonly IStaticDataService staticDataService;
        private GameObject enemyParent;
        private GameObject projectileParent;
        private Tower tower;
        public GameFactory(IStaticDataService staticDataService)
        {
            this.staticDataService = staticDataService;
        }

        public Tower CreateTower()
        {
            tower = GameObject.Instantiate<Tower>(staticDataService.TowerData.TowerTemplate);
            tower.Construct(this, staticDataService.UpgradeData);
            return tower;
        }

        public Enemy CreateEnemy(EnemyType type, Vector2 position)
        {
            if (enemyParent == null)
                enemyParent = new GameObject("Enemies");

            var data = staticDataService.GetEnemyDataFor(type);
            var enemy = GameObject.Instantiate<Enemy>(data.Template, position, Quaternion.identity, enemyParent.transform);
            enemy.GetComponent<Damageable>().Construct(data.MaxHealth);
            enemy.Construct(tower, data);
            return enemy;
        }

        public void ClearEnemies()
        {

        }

        public Projectile CreateProjectile(Vector2 position, Vector2 direction, float speed, float damage)
        {
            if (projectileParent == null)
                projectileParent = new GameObject("Projectiles");

            var projectile = GameObject.Instantiate<Projectile>(staticDataService.TowerData.ProjectileTemplate, position, Quaternion.identity, projectileParent.transform);
            projectile.Construct(direction, speed, damage);
            return projectile;
        }
    }
}