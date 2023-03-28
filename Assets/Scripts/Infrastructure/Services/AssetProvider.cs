using System.Linq;
using TowerDefense.UI;
using UnityEngine;

namespace TowerDefense.Infrastructure
{
    public class AssetProvider : IAssetProvider
    {
        private readonly AssetsData assetsData;

        public AssetProvider()
        {
            assetsData = Resources.LoadAll<AssetsData>("").FirstOrDefault();
        }

        public ResultWindow GetResultWindow()
        {
            return assetsData.ResultWindow;
        }

        public UpgradePanel GetUpgradePanel()
        {
            return assetsData.UpgradePanel;
        }

        public CoinCounter GetCoinCounter()
        {
            return assetsData.CoinCounter;
        }
    }
}
