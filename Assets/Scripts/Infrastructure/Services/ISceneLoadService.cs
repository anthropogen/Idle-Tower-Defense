using System;
using UnityEngine.SceneManagement;

namespace TowerDefense.Infrastructure
{
    public interface ISceneLoadService : IService
    {
        void LoadLevel(string name, Action onCompleted = null);
    }
}