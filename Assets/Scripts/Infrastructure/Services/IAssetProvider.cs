using TowerDefense.UI;

namespace TowerDefense.Infrastructure
{
    public interface IAssetProvider : IService
    {
        ResultWindow GetResultWindow();
        UpgradePanel GetUpgradePanel();
    }
}