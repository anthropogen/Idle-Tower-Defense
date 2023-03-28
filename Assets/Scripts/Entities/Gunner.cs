using System;
using TowerDefense.Infrastructure;
using UnityEngine;

namespace TowerDefense.Entities
{
    public class Gunner
    {
        private readonly IGameFactory gameFactory;
        private readonly CircleCollider2D circleCollider;
        private readonly ContactFilter2D contactFilter;
        private float lastShotTime;
        private RaycastHit2D[] hits = new RaycastHit2D[5];
        public float Damage { get; set; }
        public float FiringRate { get; set; }

        public Gunner(IGameFactory gameFactory, CircleCollider2D circleCollider, LayerMask enemyMask)
        {
            this.gameFactory = gameFactory;
            this.circleCollider = circleCollider;
            contactFilter = new ContactFilter2D();
            contactFilter.SetLayerMask(enemyMask);
        }

        public void Run()
        {
            if (Time.time < lastShotTime)
                return;

            if (circleCollider.Cast(Vector2.zero, contactFilter, hits) > 0)
            {
                Enemy enemy = GetNearestEnemy().GetComponent<Enemy>();
                Fire(enemy);
                Array.Clear(hits, 0, hits.Length);
            }
        }

        private void Fire(Enemy enemy)
        {
            var direction = Vector3.Normalize(enemy.transform.position - circleCollider.transform.position);
            gameFactory.CreateProjectile(circleCollider.transform.position, direction, 10, Damage);
            lastShotTime = Time.time + FiringRate;
        }

        private Collider2D GetNearestEnemy()
        {
            Collider2D result = null;
            float maxDistance = float.MaxValue;
            foreach (var item in hits)
            {
                if (item.collider == null)
                    continue;
                var sqrDistance = Vector2.SqrMagnitude(circleCollider.transform.position - item.collider.transform.position);
                if (sqrDistance < maxDistance)
                {
                    maxDistance = sqrDistance;
                    result = item.collider;
                }
            }
            return result;
        }
    }
}