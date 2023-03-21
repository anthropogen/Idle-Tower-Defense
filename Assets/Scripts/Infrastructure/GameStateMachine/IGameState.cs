namespace TowerDefense.Infrastructure
{
    public interface IGameState
    {
        void Enter();
        void Exit();
    }

    public interface IRunGameState : IGameState
    {
        void Run();
    }
}
