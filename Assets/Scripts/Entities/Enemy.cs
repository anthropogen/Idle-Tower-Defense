using NTC.Global.Cache;
using System;
using TowerDefense.StaticData;
using UnityEngine;

namespace TowerDefense.Entities
{
    public class Enemy : MonoCache
    {
        [SerializeField] private Rigidbody2D body;
        [SerializeField] private Damageable damageable;
        [SerializeField] private new Collider2D collider;
        [field: SerializeField] public EnemyType Type { get; private set; }
        private Mover mover;
        public event Action<Enemy> Died;

        public void Construct(Tower target, EnemyStaticData staticData)
            => mover = new Mover(body, target, staticData);

        protected override void OnEnabled()
        {
            damageable.Died += OnDied;
            collider.enabled = true;
        }

        protected override void OnDisabled()
        {
            damageable.Died -= OnDied;
            collider.enabled = false;
        }

        protected override void FixedRun()
            => mover?.Run();

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.TryGetComponent(out Damageable damageable))
            {
                damageable.ApplyDamage(1);
            }
        }

        private void OnDied()
            => Died?.Invoke(this);
    }
}