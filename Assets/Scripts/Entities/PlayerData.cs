using System;
using System.Collections.Generic;

namespace TowerDefense.Entities
{

    public class PlayerData
    {
        private readonly Dictionary<UpgradeType, int> upgradeLevels;
        private int coins;
        public event Action<UpgradeType> UpgradeLevelChanged;
        public event Action<int> CoinsChanged;
        public int Coins
        {
            get { return coins; }
            set
            {
                coins = value;
                CoinsChanged?.Invoke(coins);
            }
        }

        public int this[UpgradeType type]
        {
            get
            {
                return upgradeLevels[type];
            }
            set
            {
                upgradeLevels[type] = value;
                UpgradeLevelChanged?.Invoke(type);
            }
        }

        public PlayerData()
        {
            upgradeLevels = new Dictionary<UpgradeType, int>();
            SetZeroLevels();
        }



        public void Reset()
        {
            Coins = 0;
            SetZeroLevels();
        }

        private void SetZeroLevels()
        {
            foreach (var item in Enum.GetValues(typeof(UpgradeType)))
                upgradeLevels[(UpgradeType)item] = 0;
        }
    }

}