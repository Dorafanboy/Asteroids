using System;
using Infrastructure.Services.Factories;

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
            SpawnShip(); // здесь будем спавнить корабль и передавать его в качестве цели для врагов
        }

        public void Exit()
        {
            
        }

        private void SpawnShip()
        {
            var ship = _factory.CreateShip();
        }
    }
}