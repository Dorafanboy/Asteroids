using Guns;
using Infrastructure.Services.Factories;
using Infrastructure.Wrapper;
using ShipContent;
using UnityEngine;

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

        public void Enter() // сделать дженерик enter'a принимающий гантайп
        {
            var firstWeapon = _factory.CreateProjectileWeapon(GunType.Projectile);
            var secondWeapon = _factory.CreateLaserWeapon(GunType.Laser); 
            var ship = SpawnShip(firstWeapon, secondWeapon); 
            var wrapper = CreateWrapper(ship);
            
            _stateMachine.Enter<GameBehaviourState>();
        }

        private Ship SpawnShip<T, TT>(T firstWeapon, TT secondWeapon) where T : Weapon<Bullet> where TT : Weapon<Bullet>
        {
            return _factory.CreateShip(firstWeapon, secondWeapon);
        }

        public void Exit()
        {
        }

        private ScreenWrapper CreateWrapper(Ship ship)
        {
            return _factory.CreateWrapper(ship);
        }
    }
}