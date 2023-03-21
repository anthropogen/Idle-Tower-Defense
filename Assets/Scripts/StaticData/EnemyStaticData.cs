using TowerDefense.Entities;
using UnityEngine;

namespace TowerDefense.StaticData
{
    [CreateAssetMenu(fileName = "newEnemyData", menuName = "Static Data/Enemy", order = 51)]
    public class EnemyStaticData : ScriptableObject
    {
        [field: SerializeField] public EnemyType Type { get; private set; }
        [field: SerializeField, Min(0)] public float Speed { get; private set; }
        [field: SerializeField, Min(0)] public float Damage { get; private set; }
        [field: SerializeField] public Enemy Template { get; private set; }
    }
}