using Entities.Guns;
using Entities.Ship;
using Infrastructure.Services.Containers;
using Infrastructure.Services.Factories;
using Infrastructure.Wrapper;

namespace Infrastructure.States
{
    public class InitialState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly WeaponFactory _weaponFactory;
        private readonly SpawnerFactory _spawnerFactory;
        private readonly ShipFactory _shipFactory;

        public InitialState(StateMachine stateMachine, IDiContainer diContainer)
        {
            _stateMachine = stateMachine;
            _weaponFactory = diContainer.GetService<WeaponFactory>();
            _spawnerFactory = diContainer.GetService<SpawnerFactory>();
            _shipFactory = diContainer.GetService<ShipFactory>();
        }

        public void Enter() // сделать дженерик enter'a принимающий гантайп
        {
            var firstWeapon = _weaponFactory.CreateProjectileWeapon(BulletType.Projectile);
            var secondWeapon = _weaponFactory.CreateLaserWeapon(BulletType.Laser); 
            var ship = SpawnShip(firstWeapon, secondWeapon); //WTF
            var enemySpawner = _spawnerFactory.CreateEnemySpawner(ship.Prefab.transform);

            _stateMachine.Enter<GameBehaviourState>();
        }

        public void Exit()
        {
        }

        private ShipModel SpawnShip<T, TT>(T firstWeapon, TT secondWeapon) where T 
            : WeaponBase<Bullet> where TT : WeaponBase<Bullet>
        {
            return _shipFactory.CreateShip(firstWeapon, secondWeapon);
        }
    }
}