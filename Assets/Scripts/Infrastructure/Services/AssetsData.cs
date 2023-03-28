using TowerDefense.UI;
using UnityEngine;

namespace TowerDefense.Infrastructure
{
    [CreateAssetMenu(fileName = "newAssets", menuName = "AssetsConfig/Create New Assets Config", order = 51)]
    public class AssetsData : ScriptableObject
    {
        [field: SerializeField] public ResultWindow ResultWindow { get; private set; }
        [field: SerializeField] public UpgradePanel upgradePanel { get; private set; }
    }
}
