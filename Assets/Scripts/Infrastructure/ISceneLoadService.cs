using System;

namespace TowerDefense.Infrastructure
{
    public interface ISceneLoadService : IService
    {
        void LoadLevel(string name, Action onCompleted = null);
    }
}