using TowerDefense.Entities;

namespace TowerDefense.Infrastructure
{
    public class FinishState : IGameState
    {
        private readonly IGameFactory gameFactory;
        private readonly GameStateMachine stateMachine;
        private readonly PlayerData playerData;

        public FinishState(IGameFactory gameFactory, GameStateMachine stateMachine, Entities.PlayerData playerData)
        {
            this.gameFactory = gameFactory;
            this.stateMachine = stateMachine;
            this.playerData = playerData;
        }

        public void Enter()
        {
            gameFactory.CreateResultWindow(stateMachine);
        }

        public void Exit()
        {
            playerData.Reset();
        }
    }
}
