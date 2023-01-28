namespace Infrastructure.States
{
    public interface IStateMachine
    {
        void Enter<TState>() where TState : IState;
        void Exit<TState>() where TState : IState;
    }
}