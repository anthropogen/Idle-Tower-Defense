using TowerDefense.UI;

namespace TowerDefense.Infrastructure
{
    public interface IAssetProvider : IService
    {
        CoinCounter GetCoinCounter();
        ResultWindow GetResultWindow();
        UpgradePanel GetUpgradePanel();
    }
}