using System.Collections.Generic;
using System.Linq;
using TowerDefense.Entities;
using TowerDefense.Infrastructure;
using TowerDefense.StaticData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TowerDefense.EnemyWaves
{
    public class EnemySpawner
    {
        private readonly IGameFactory gameFactory;
        private readonly List<SpawnCounter> spawnInfo;
        private readonly HashSet<Enemy> spawnedEnemies;
        private readonly Camera cam;
        public bool IsStopSpawn { get; private set; }

        public EnemySpawner(IGameFactory gameFactory, Wave enemyWave)
        {
            this.gameFactory = gameFactory;
            spawnedEnemies = new HashSet<Enemy>();
            cam = Camera.main;
            spawnInfo = enemyWave.Enemies.Select(w => new SpawnCounter
            {
                Type = w.Type,
                Count = w.Count,
                SpawnDelay = w.SpawnDelay,
                PrevSpawnTime = NextSpawnTime(w.SpawnDelay.x, w.SpawnDelay.y)
            }).ToList();
        }


        public void Run()
        {
            if (spawnedEnemies.Count == 0 && spawnInfo.Count == 0)
            {
                IsStopSpawn = true;
                return;
            }

            if (spawnInfo.Count == 0)
                return;


            for (int i = spawnInfo.Count - 1; i >= 0; i--)
            {
                var counter = spawnInfo[i];
                if (counter.Count <= 0)
                {
                    spawnInfo.RemoveAt(i);
                    continue;
                }
                if (counter.PrevSpawnTime <= Time.time)
                {
                    Spawn(counter.Type);
                    counter.Count--;
                    counter.PrevSpawnTime = NextSpawnTime(counter.SpawnDelay.x, counter.SpawnDelay.y);
                }

            }
        }

        private void Spawn(EnemyType type)
        {
            var enemy = gameFactory.CreateEnemy(type, GetSpawnPosition());
            enemy.Died += OnEnemyDied;
            spawnedEnemies.Add(enemy);
        }

        private void OnEnemyDied(Enemy enemy)
        {
            enemy.Died -= OnEnemyDied;
            enemy.gameObject.SetActive(false);
            spawnedEnemies.Remove(enemy);
        }

        private Vector3 GetSpawnPosition()
        {
            var random = Random.insideUnitCircle.normalized;
            var viewPortPos = random + new Vector2(0.5f, 0.5f);
            var pos = cam.ViewportToWorldPoint(viewPortPos);
            pos.z = 0;
            //Debug.Log($"<color=red>RandomCircle {random} </color> <color=blue>ViewPort {viewPortPos} </color> <color=green>Pos {pos} </color>");
            return pos;
        }

        private float NextSpawnTime(float x, float y)
            => Time.time + Random.Range(x, y);

        private class SpawnCounter
        {
            public EnemyType Type;
            public int Count;
            public float PrevSpawnTime;
            public Vector2 SpawnDelay;
        }
    }
}