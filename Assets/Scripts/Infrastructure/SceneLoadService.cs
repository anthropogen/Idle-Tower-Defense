using System;
using System.Collections;
using UnityEngine.SceneManagement;

namespace TowerDefense.Infrastructure
{
    public class SceneLoadService : ISceneLoadService
    {
        private readonly Bootstrapper bootstrapper;

        public SceneLoadService(Bootstrapper bootstrapper)
        {
            this.bootstrapper = bootstrapper;
        }

        public void LoadLevel(string name, Action onCompleted = null)
            => bootstrapper.StartCoroutine(LoadLevelCoroutine(name, onCompleted));


        private IEnumerator LoadLevelCoroutine(string name, Action onCompleted = null)
        {
            var operation = SceneManager.LoadSceneAsync(name);
            operation.allowSceneActivation = false;
            while (!operation.isDone)
            {
                yield return null;
            }
            operation.allowSceneActivation = true;
            onCompleted?.Invoke();
        }
    }
}
