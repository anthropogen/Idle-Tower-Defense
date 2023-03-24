using NTC.Global.Cache;
using System;
using TowerDefense.StaticData;
using UnityEngine;

namespace TowerDefense.Entities
{
    public class Enemy : MonoCache
    {
        [SerializeField] private Rigidbody2D body;
        private Mover mover;
        public event Action<Enemy> Died;

        public void Construct(Tower target, EnemyStaticData staticData)
        {
            mover = new Mover(body, target, staticData);
        }

        protected override void FixedRun()
        {
            mover?.Run();
        }
    }
}