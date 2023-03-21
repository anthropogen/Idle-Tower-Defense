using NTC.Global.Cache;


namespace TowerDefense.Infrastructure
{
    public sealed class Bootstrapper : MonoCache
    {
        private GameStateMachine stateMachine;
        private ServiceLocator serviceLocator;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            CreateGameStateMachine();
        }

        protected override void Run()
            => stateMachine.Run();

        private void CreateGameStateMachine()
        {
            serviceLocator = new ServiceLocator();
            stateMachine = new GameStateMachine(serviceLocator, this);
            stateMachine.Change<BootstrapState>();
        }
    }
}