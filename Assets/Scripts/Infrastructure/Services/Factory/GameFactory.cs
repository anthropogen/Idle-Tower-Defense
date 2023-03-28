using NTC.Global.System;
using System.Collections.Generic;
using TowerDefense.Entities;
using TowerDefense.UI;
using UnityEngine;
using static TowerDefense.Entities.Tower;

namespace TowerDefense.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private readonly IStaticDataService staticDataService;
        private readonly IAssetProvider assetProvider;
        private GameObject enemyParent;
        private GameObject projectileParent;
        private Tower tower;
        private List<Enemy> spawnedEnemies = new List<Enemy>();

        public Tower Tower => tower;

        public GameFactory(IStaticDataService staticDataService, IAssetProvider assetProvider)
        {
            this.staticDataService = staticDataService;
            this.assetProvider = assetProvider;
        }

        public Tower CreateTower(PlayerData playerData)
        {
            tower = GameObject.Instantiate<Tower>(staticDataService.TowerData.TowerTemplate);
            tower.Construct(this, staticDataService.UpgradeData, playerData);
            tower.Get<Damageable>().Construct(staticDataService.TowerData.MaxHealth);
            tower.transform.position = new Vector3(0, 1);
            return tower;
        }

        public Enemy CreateEnemy(EnemyType type, Vector2 position)
        {
            if (enemyParent == null)
            {
                enemyParent = new GameObject("Enemies");
            }

            var data = staticDataService.GetEnemyDataFor(type);
            var enemy = GameObject.Instantiate<Enemy>(data.Template, position, Quaternion.identity, enemyParent.transform);
            enemy.Get<Damageable>().Construct(data.MaxHealth);
            enemy.Construct(tower, data);
            spawnedEnemies.Add(enemy);
            return enemy;
        }

        public void ClearEnemies()
        {
            foreach (var item in spawnedEnemies)
            {
                item.Disable();
            }
            spawnedEnemies.Clear();
        }

        public Projectile CreateProjectile(Vector2 position, Vector2 direction, float speed, float damage)
        {
            if (projectileParent == null)
            {
                projectileParent = new GameObject("Projectiles");
            }

            var projectile = GameObject.Instantiate<Projectile>(staticDataService.TowerData.ProjectileTemplate, position, Quaternion.identity, projectileParent.transform);
            projectile.Construct(direction, speed, damage);
            return projectile;
        }

        public void CreateResultWindow(GameStateMachine stateMachine)
        {
            var window = GameObject.Instantiate(assetProvider.GetResultWindow());
            window.Construct(stateMachine);
        }

        public UpgradePanel CreateUpgradePanel(PlayerData playerData)
        {
            var panel = GameObject.Instantiate(assetProvider.GetUpgradePanel());
            panel.Construct(playerData, staticDataService.UpgradeData);
            return panel;
        }

        public CoinCounter CreateCounter(PlayerData playerData)
        {
            var counter = GameObject.Instantiate(assetProvider.GetCoinCounter());
            counter.Construct(playerData);
            return counter;
        }
    }
}