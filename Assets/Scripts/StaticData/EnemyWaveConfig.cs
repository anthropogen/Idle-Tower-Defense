using TowerDefense.Entities;
using UnityEngine;

namespace TowerDefense.StaticData
{
    [System.Serializable]
    public class EnemyWaveConfig
    {
        [field: SerializeField] public EnemyType Type { get; private set; }
        [field: SerializeField, Min(0)] public int Count { get; private set; }
        [field: SerializeField] public Vector2 SpawnDelay { get; private set; }

    }
}