using UnityEngine;

namespace TowerDefense.StaticData
{
    [System.Serializable]
    public class Wave
    {
        [field: SerializeField] public EnemyWaveConfig[] Enemies { get; private set; }

    }
}