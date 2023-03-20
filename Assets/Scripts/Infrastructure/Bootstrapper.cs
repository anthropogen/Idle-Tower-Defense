using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Infrastructure
{
    public sealed class Bootstrapper : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}