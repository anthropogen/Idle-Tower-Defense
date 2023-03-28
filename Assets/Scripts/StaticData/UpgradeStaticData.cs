using System.Collections.Generic;
using System.Linq;
using TowerDefense.Entities;
using UnityEngine;
using static TowerDefense.StaticData.UpgradeStaticData.UpgradeData;

namespace TowerDefense.StaticData
{
    [CreateAssetMenu(fileName = "newUpgradeData", menuName = "Static Data/Upgrade", order = 51)]

    public class UpgradeStaticData : ScriptableObject
    {
        [SerializeField] private UpgradeData[] upgrades;
        private Dictionary<UpgradeType, UpgradeValue[]> values;

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

        public UpgradeValue GetUpgradeValueFor(UpgradeType type, int level)
        {
            if (values == null)
                CreateValuesDictionary();

            if (values.TryGetValue(type, out var value))
            {
                level = Mathf.Min(level, value.Length - 1);
                return value[level];
            }
            return null;
        }

        private void CreateValuesDictionary()
            => values = upgrades.ToDictionary(u => u.Type, u => u.Values);

        [System.Serializable]
        public class UpgradeData
        {
            [field: SerializeField] public UpgradeType Type { get; private set; }
            [field: SerializeField] public UpgradeValue[] Values { get; private set; }

            [System.Serializable]
            public class UpgradeValue
            {
                [field: SerializeField] public float Value { get; private set; }
                [field: SerializeField] public int Price { get; private set; }
            }
        }
    }
}