using NTC.Global.Cache;
using System;
using System.Collections.Generic;
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
        private PlayerData playerData;
        private UpgradeStaticData upgradeStaticData;
        private Dictionary<UpgradeType, Action> upgradeAction;

        public void Construct(IGameFactory gameFactory, UpgradeStaticData upgradeData, PlayerData playerData)
        {
            this.playerData = playerData;
            upgradeStaticData = upgradeData;
            gunner = new Gunner(gameFactory, rangeCollider, enemyMask);

            CreateUpgradeActions();
            playerData.UpgradeLevelChanged += Upgrade;
            foreach (var item in Enum.GetValues(typeof(UpgradeType)))
                Upgrade((UpgradeType)item);
        }

        protected override void OnDisabled()
        {
            if (playerData != null)
                playerData.UpgradeLevelChanged -= Upgrade;
        }

        protected override void FixedRun()
        {
            gunner.Run();
        }

        private void SetRange(float range)
        {
            rangeCollider.transform.localScale = new Vector3(range, range);
        }

        private void SetDamage(float damage)
        {
            gunner.Damage = damage;
        }

        private void SetFiringRate(float firingRate)
        {
            gunner.FiringRate = firingRate;
        }

        private void Upgrade(UpgradeType type)
        {
            if (!upgradeAction.ContainsKey(type))
                return;
            upgradeAction[type].Invoke();
        }

        private void CreateUpgradeActions()
        {
            upgradeAction = new Dictionary<UpgradeType, Action>();
            upgradeAction[UpgradeType.Damage] = () =>
            {
                SetDamage(upgradeStaticData.GetUpgradeValueFor(UpgradeType.Damage, playerData[UpgradeType.Damage]).Value);
            };
            upgradeAction[UpgradeType.FiringRate] = () =>
            {
                SetFiringRate(upgradeStaticData.GetUpgradeValueFor(UpgradeType.FiringRate, playerData[UpgradeType.FiringRate]).Value);
            };
            upgradeAction[UpgradeType.Range] = () =>
            {
                SetRange(upgradeStaticData.GetUpgradeValueFor(UpgradeType.Range, playerData[UpgradeType.Range]).Value);
            };
        }
    }
}