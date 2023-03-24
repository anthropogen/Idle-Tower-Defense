using NTC.Global.Cache;
using System;
using System.Collections.Generic;

namespace TowerDefense.Entities
{
    public class Enemy : MonoCache
    {
        public event Action<Enemy> Died;
    }

    public enum EnemyType
    {
        Simple = 0,
        Fast = 1
    }
}