using UnityEngine;

namespace TowerDefense.StaticData
{
    [CreateAssetMenu(fileName = "newWavesConfig", menuName = "Static Data/Waves", order = 51)]
    public class WaveStaticData : ScriptableObject
    {
        [SerializeField] private Wave[] waves;
        public int Count => waves.Length;
        public Wave this[int index]
        {
            get { return waves[index]; }
        }
    }
}