using TowerDefense.Entities;
using UnityEngine;

namespace TowerDefense.StaticData
{
    [CreateAssetMenu(fileName = "newTowerData", menuName = "Static Data/Tower", order = 51)]
    public class TowerStaticData : ScriptableObject
    {
        [field: SerializeField] public Tower TowerTemplate { get; private set; }
        [field: SerializeField] public Projectile ProjectileTemplate { get; private set; }
    }
}