using System.Collections.Generic;
using Constants;
using Entities.Enemy;
using Entities.Guns;
using Entities.Ship;
using UnityEngine;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Inputs;
using Infrastructure.Spawners;
using Infrastructure.Spawners.SpawnPoints;
using Infrastructure.Wrapper;
using StaticData;
using StaticData.Settings;
using Bullet = Entities.Guns.Bullet;

namespace Infrastructure.Services.Factories
{
    public class Factory : IFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IUpdatable _updatable;
        private readonly IInputService _inputService;

        public Factory(IAssetProvider assetProvider, IUpdatable updatable, IInputService inputService)
        {
            _assetProvider = assetProvider;
            _updatable = updatable;
            _inputService = inputService;
        }

        public ShipModel CreateShip<T, TT>(T firstWeapon, TT secondWeapon) where T : Weapon<Bullet> where TT : Weapon<Bullet>
        {
            var shipData = _assetProvider.GetData<ShipStaticData>(AssetPath.ShipPath);
            var shipPrefab = Object.Instantiate(shipData.Prefab, Vector3.zero, Quaternion.identity);

            var ship = new ShipModel(shipData.Acceleration, shipData.Deceleration, shipData.MaxSpeed, shipData.RotationSpeed,
                shipPrefab, _inputService, firstWeapon, secondWeapon);

            var shipView = new ShipView(ship);
            var shipPresenter = new ShipPresenter(ship, shipView);

            return ship;
        }

        public ScreenWrapper CreateWrapper(ShipModel shipModel)
        {
            var wrapper = new ScreenWrapper(_updatable, shipModel);
            wrapper.Enable();

            return wrapper;
        }

        public ProjectileWeapon CreateProjectileWeapon(GunType gunType)
        {
            GetStats(gunType, out var pool, out var weaponData, out var shotCount);
            var weapon = new ProjectileWeapon(pool, _updatable, gunType, weaponData.FireCooldown);

            return weapon;
        }

        public LaserWeapon CreateLaserWeapon(GunType gunType)
        {
            GetStats(gunType, out var pool, out var weaponData, out var shotCount);
            var weapon = new LaserWeapon(pool, _updatable, gunType, weaponData.FireCooldown, shotCount);

            return weapon;
        }

        public EnemySpawner CreateEnemySpawner(Transform prefabTransform)
        {
            var poolData = _assetProvider.GetData<PoolStaticData>(AssetPath.PoolPath);

            var settings = _assetProvider.GetData<EnemySpawnerSettings>(AssetPath.EnemySpawnerSettings);
            var pool = new AsteroidObjectPool<EnemyEntityBase>(settings.EnemyCount, CreateUfo, CreateAsteroid);
            var enemySpawner = new EnemySpawner(pool, prefabTransform, settings, CreateSpawnPointsContainer(), _updatable);
            var screenWrapper = new AsteroidScreenWrapper<EnemyEntityBase>(_updatable, pool, enemySpawner);

            return enemySpawner;
        }

        private EnemyEntityBase CreateObject()
        {
            throw new System.NotImplementedException();
        }

        private SpawnPointsContainer CreateSpawnPointsContainer()
        {
            var list = new List<ISpawnBehaviour>()
            {
                new TopSpawnBehaviour(SpawnPoints.TopEdge),
                new BottomSpawnBehaviour(SpawnPoints.BottomEdge),
                new RightSpawnBehaviour(SpawnPoints.LeftEdge),
                new LeftSpawnBehaviour(SpawnPoints.RightEdge),
            };

            return new SpawnPointsContainer(list);
        }

        public Ufo CreateUfo(Transform playerShip)
        {
            var ufoData = _assetProvider.GetData<EnemyStaticData>(AssetPath.Ufo);
            var asteroidPrefab = Object.Instantiate(ufoData.Prefab);
            var ufo = new Ufo(asteroidPrefab, ufoData.Speed, playerShip, _updatable);
            asteroidPrefab.gameObject.SetActive(false);

            return ufo;
        }

        public Asteroid CreateAsteroid(Transform playerShip)
        {
            var asteroidData = _assetProvider.GetData<EnemyStaticData>(AssetPath.Asteroid);
            var asteroidPrefab = Object.Instantiate(asteroidData.Prefab, Vector3.zero, Quaternion.identity);
            var asteroid = new Asteroid(asteroidPrefab, asteroidData.Speed, _updatable, Camera.main);
            asteroidPrefab.gameObject.SetActive(false);

            return asteroid;
        }

        private void GetStats(GunType gunType, out ObjectPool<Bullet> pool, out BulletStaticData weaponData,
            out int shotCount)
        {
            var poolData = _assetProvider.GetData<PoolStaticData>(AssetPath.PoolPath);
            weaponData = gunType == GunType.Projectile
                ? _assetProvider.GetData<BulletStaticData>(AssetPath.Projectile)
                : _assetProvider.GetData<BulletStaticData>(AssetPath.Laser);

            pool = new ObjectPool<Bullet>(poolData.PoolSize, CreateBullet<Bullet>);
            shotCount = weaponData.ShotsCount;
        }

        private T CreateBullet<T>(GunType gunType) where T : Bullet
        {
            var bulletData = gunType == GunType.Projectile
                ? _assetProvider.GetData<BulletStaticData>(AssetPath.Projectile)
                : _assetProvider.GetData<BulletStaticData>(AssetPath.Laser);

            var bulletPrefab = Object.Instantiate(bulletData.Prefab);
            bulletPrefab.SetActive(false);

            var bullet = new Bullet(bulletData.Deceleration, _updatable, bulletPrefab, bulletData.GunType);
            var bulletView = new ProjectileView();
            var bulletPresenter = new ProjectilePresenter(bullet, bulletView, _updatable);

            return bullet as T;
        }
    }
}