using NTC.Global.Cache;
using TowerDefense.Entities;
using TowerDefense.StaticData;
using UnityEngine;

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

            foreach (var button in upgradeButtons)
            {
                int level = playerData[button.Type];
                var upgrade = upgradeStaticData.GetUpgradeValueFor(button.Type, level + 1);
                RedrawButton(button, upgrade);
            }
        }


        protected override void OnEnabled()
        {
            foreach (var button in upgradeButtons)
            {
                button.OnClick += OnClickUpgradeButton;
            }
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
                RedrawButton(button, nextUpgrade);
            }
            else
            {
                button.SetInteractable(false, false);
            }
            playerData.Coins -= upgrade.Price;
        }

        private static void RedrawButton(UpgradeButton button, UpgradeStaticData.UpgradeData.UpgradeValue nextUpgrade)
        {
            button.SetDescription(nextUpgrade.Price.ToString(), nextUpgrade.Value.ToString());
        }

        private void OnCoinsChanged(int coins)
        {
            foreach (var button in upgradeButtons)
            {
                if (!button.HasNextUpgrade)
                    continue;
                int level = playerData[button.Type];
                var upgrade = upgradeStaticData.GetUpgradeValueFor(button.Type, level + 1);
                button.SetInteractable(upgrade.Price <= playerData.Coins, true);
            }
        }
    }
}