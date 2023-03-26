using NTC.Global.Cache;
using TowerDefense.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.UI
{
    public class ResultWindow : MonoCache
    {
        [SerializeField] private Button retryButton;
        private GameStateMachine stateMachine;

        public void Construct(GameStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
            retryButton.onClick.AddListener(RetryGame);
        }

        private void RetryGame()
        {
            retryButton.onClick.RemoveListener(RetryGame);
            stateMachine.Change<LoadLevelState>();
        }
    }
}