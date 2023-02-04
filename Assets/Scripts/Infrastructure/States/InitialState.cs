using Infrastructure.Services.Factories;
using Infrastructure.Wrapper;
using ShipContent;

namespace Infrastructure.States
{
    public class InitialState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly IFactory _factory;

        public InitialState(StateMachine stateMachine, IFactory factory)
        {
            _stateMachine = stateMachine;
            _factory = factory;
        }

        public void Enter()
        {
            var ship = SpawnShip(); 
            var wrapper = CreateWrapper(ship);
            
            _stateMachine.Enter<GameBehaviourState>();
        }

        public void Exit()
        {
        }

        private Ship SpawnShip()
        {
            return _factory.CreateShip();
        }

        private ScreenWrapper CreateWrapper(Ship ship)
        {
            return _factory.CreateWrapper(ship);
        }
    }
}