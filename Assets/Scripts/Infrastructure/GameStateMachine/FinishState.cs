namespace TowerDefense.Infrastructure
{
    public class FinishState : IGameState
    {
        private readonly IGameFactory gameFactory;
        private readonly GameStateMachine stateMachine;

        public FinishState(IGameFactory gameFactory, GameStateMachine stateMachine)
        {
            this.gameFactory = gameFactory;
            this.stateMachine = stateMachine;
        }

        public void Enter()
        {
            gameFactory.CreateResultWindow(stateMachine);
        }

        public void Exit()
        {
        }
    }
}
