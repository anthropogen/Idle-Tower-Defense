using NTC.Global.Cache;
using System;
using UnityEngine;

namespace TowerDefense.Entities
{
    public class Damageable : MonoCache
    {
        private float maxHealth;
        private float currentHealht;
        public event Action<float, float> DamageRecieved;
        public event Action Died;

        public void Construct(float maxHealth)
        {
            if (maxHealth < 0)
                throw new ArgumentException("health cannot be less than 0");
            this.maxHealth = maxHealth;
            currentHealht = maxHealth;
        }

        public void ApplyDamage(float damage)
        {
            if (damage < 0)
                throw new ArgumentException("damage cannot be less than 0");
            currentHealht = Mathf.Max(0, currentHealht - damage);
            if (currentHealht == 0)
                Died?.Invoke();
            else
                DamageRecieved?.Invoke(currentHealht, maxHealth);
        }
    }
}