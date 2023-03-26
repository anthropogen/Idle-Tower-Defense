using System.Collections.Generic;
using System.Linq;
using TowerDefense.Entities;
using UnityEngine;

namespace TowerDefense.StaticData
{
    [CreateAssetMenu(fileName = "newUpgradeData", menuName = "Static Data/Upgrade", order = 51)]

    public class UpgradeStaticData : ScriptableObject
    {
        [SerializeField] private UpgradeData[] upgrades;
        private Dictionary<UpgradeType, float[]> values;

        public bool HasUpgradeFor(UpgradeType type, int level)
        {
            if (values == null)
                CreateValuesDictionary();

            if (values.TryGetValue(type, out var value))
            {
                if (value.Length < level)
                    return true;
            }
            return false;
        }

        public float GetUpgradeValueFor(UpgradeType type, int level)
        {
            if (values == null)
                CreateValuesDictionary();

            if (values.TryGetValue(type, out var value))
            {
                level = Mathf.Min(level, value.Length - 1);
                return value[level];
            }
            return -1;
        }

        private void CreateValuesDictionary()
            => values = upgrades.ToDictionary(u => u.Type, u => u.Values);

        [System.Serializable]
        public class UpgradeData
        {
            [field: SerializeField] public UpgradeType Type { get; private set; }
            [field: SerializeField] public float[] Values { get; private set; }
        }
    }
}