using NTC.Global.Cache;
using TowerDefense.Entities;
using TowerDefense.StaticData;
using UnityEngine;
using static TowerDefense.Entities.Tower;

namespace TowerDefense.UI
{
    public class UpgradePanel : MonoCache
    {
        [SerializeField] private UpgradeButton[] upgradeButtons;
        private PlayerData playerData;
        private UpgradeStaticData upgradeStaticData;

        public void Construct(PlayerData playerData, UpgradeStaticData upgradeStaticData)
        {
            this.playerData = playerData;
            this.upgradeStaticData = upgradeStaticData;
            playerData.CoinsChanged += OnCoinsChanged;
            OnCoinsChanged(playerData.Coins);
        }


        protected override void OnEnabled()
        {
            foreach (var button in upgradeButtons)
            {
                button.OnClick += OnClickUpgradeButton;
            }
            if (playerData != null)
                playerData.CoinsChanged += OnCoinsChanged;
        }

        protected override void OnDisabled()
        {
            foreach (var button in upgradeButtons)
            {
                button.OnClick -= OnClickUpgradeButton;
            }
            if (playerData != null)
                playerData.CoinsChanged -= OnCoinsChanged;
        }

        private void OnClickUpgradeButton(UpgradeButton button)
        {
            var upgradeType = button.Type;
            int level = ++playerData[upgradeType];
            var upgrade = upgradeStaticData.GetUpgradeValueFor(upgradeType, level);
            if (upgradeStaticData.HasUpgradeFor(upgradeType, level + 1))
            {
                var nextUpgrade = upgradeStaticData.GetUpgradeValueFor(upgradeType, level + 1);
                button.SetDescription(nextUpgrade.Price.ToString(), nextUpgrade.Value.ToString());
            }
            else
            {
                button.SetInteractable(false, false);
            }
            playerData.Coins -= upgrade.Price;
        }

        private void OnCoinsChanged(int coins)
        {

        }
    }
}