using NTC.Global.Cache;
using TowerDefense.Infrastructure;
using TowerDefense.StaticData;
using UnityEngine;

namespace TowerDefense.Entities
{
    public class Tower : MonoCache
    {
        [SerializeField] private CircleCollider2D rangeCollider;
        [SerializeField] private LayerMask enemyMask;
        private Gunner gunner;

        public void Construct(IGameFactory gameFactory, UpgradeStaticData upgradeData)
        {
            gunner = new Gunner(gameFactory, upgradeData, rangeCollider, enemyMask);
        }

        protected override void FixedRun()
        {
            gunner.Run();
        }
    }
}