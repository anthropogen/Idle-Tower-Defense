using System.Collections.Generic;
using System.Linq;
using TowerDefense.Entities;
using TowerDefense.StaticData;
using UnityEngine;
using System;

namespace TowerDefense.Infrastructure
{
    public class StaticDataService : IStaticDataService
    {
        private readonly TowerStaticData towerData;
        private readonly WaveStaticData wavesData;
        private readonly Dictionary<EnemyType, EnemyStaticData> enemyData;
        public TowerStaticData TowerData => towerData;
        public WaveStaticData WavesData => wavesData;

        public StaticDataService()
        {
            towerData = Resources.LoadAll<TowerStaticData>("").FirstOrDefault();
            enemyData = Resources.LoadAll<EnemyStaticData>("").ToDictionary(s => s.Type, s => s);
            wavesData = Resources.LoadAll<WaveStaticData>("").FirstOrDefault();
        }

        public EnemyStaticData GetEnemyDataFor(EnemyType type)
        {
            if (enemyData.TryGetValue(type, out var data))
                return data;
            throw new InvalidOperationException($"Doesn't have data for {type}");
        }
    }
}
