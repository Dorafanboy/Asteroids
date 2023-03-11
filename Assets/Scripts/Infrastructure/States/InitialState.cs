﻿using Entities.Enemy;
using Entities.Guns;
using Entities.Ship;
using Infrastructure.Services.Factories;
using Infrastructure.Wrapper;
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

           // CreateEntites();
        }

        public void Enter() // сделать дженерик enter'a принимающий гантайп
        {
            var firstWeapon = _factory.CreateProjectileWeapon(GunType.Projectile);
            var secondWeapon = _factory.CreateLaserWeapon(GunType.Laser); 
            var ship = SpawnShip(firstWeapon, secondWeapon); 
            var wrapper = CreateWrapper(ship);
            var enemySpawner = _factory.CreateEnemySpawner(ship.Prefab.transform);
            
            _stateMachine.Enter<GameBehaviourState>();
        }

        private void CreateEntites()
        {
            var firstWeapon = _factory.CreateProjectileWeapon(GunType.Projectile);
            var secondWeapon = _factory.CreateLaserWeapon(GunType.Laser); 
            var ship = SpawnShip(firstWeapon, secondWeapon); 
            var wrapper = CreateWrapper(ship);
            var enemySpawner = _factory.CreateEnemySpawner(ship.Prefab.transform);
        }

        private ShipModel SpawnShip<T, TT>(T firstWeapon, TT secondWeapon) where T : Weapon<Bullet> where TT : Weapon<Bullet>
        {
            return _factory.CreateShip(firstWeapon, secondWeapon);
        }

        public void Exit()
        {
        }

        private ScreenWrapper CreateWrapper(ShipModel shipModel)
        {
            return _factory.CreateWrapper(shipModel);
        }
    }
}