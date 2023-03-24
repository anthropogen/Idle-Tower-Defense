using TowerDefense.StaticData;
using UnityEngine;

namespace TowerDefense.Entities
{
    public class Mover
    {
        private readonly Rigidbody2D body;
        private readonly Tower target;
        private readonly EnemyStaticData staticData;

        public Mover(Rigidbody2D body, Tower target, EnemyStaticData staticData)
        {
            this.body = body;
            this.target = target;
            this.staticData = staticData;
        }

        public void Run()
        {
            var direction = (target.transform.position - body.transform.position).normalized;
            body.velocity = direction * staticData.Speed * Time.fixedDeltaTime;
        }
    }
}