using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Infrastructure
{
    public sealed class Bootstrapper : MonoBehaviour
    {
        private GameStateMachine stateMachine;
        private ServiceLocator serviceLocator;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            CreateGameStateMachine();
        }

        private void CreateGameStateMachine()
        {
            serviceLocator = new ServiceLocator();
            stateMachine = new GameStateMachine(serviceLocator,this);
            stateMachine.Change<BootstrapState>();
        }
    }
}