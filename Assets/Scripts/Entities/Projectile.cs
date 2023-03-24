using NTC.Global.Cache;
using UnityEngine;

namespace TowerDefense.Entities
{
    public class Projectile : MonoCache
    {
        [SerializeField] private Rigidbody2D body;
        private Vector2 direction;
        private float speed;
        private float damage;

        public void Construct(Vector2 direction, float speed, float damage)
        {
            this.direction = direction;
            this.speed = speed;
            this.damage = damage;
        }

        protected override void FixedRun()
            => body.velocity = direction * speed * Time.fixedDeltaTime;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Damageable damageable))
            {
                damageable.ApplyDamage(damage);
            }
            gameObject.SetActive(false);
        }
    }
}