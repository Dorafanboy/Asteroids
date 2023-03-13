// using System;
// using System.Collections.Generic;
// using Constants;
// using Entities.Enemy;
// using Entities.Guns;
// using Entities.Pool;
// using Entities.Ship;
// using UnityEngine;
// using Infrastructure.Services.Assets;
// using Infrastructure.Services.Containers;
// using Infrastructure.Services.Inputs;
// using Infrastructure.Spawners;
// using Infrastructure.Spawners.SpawnPoints;
// using Infrastructure.Wrapper;
// using StaticData;
// using StaticData.Settings;
// using Bullet = Entities.Guns.Bullet;
// using Object = UnityEngine.Object;
//
// namespace Infrastructure.Services.Factories
// {
//     public class Factory : IFactory
//     {
//         private readonly IAssetProvider _assetProvider;
//         private readonly IUpdatable _updatable;
//         private readonly IInputService _inputService;
//         private readonly EventListenerContainer _eventListenerContainer;
//         public event Action<IEventListener> Spawned;
//
//         public Factory(IAssetProvider assetProvider, IUpdatable updatable, IInputService inputService,
//             EventListenerContainer eventListenerContainer)
//         {
//             _assetProvider = assetProvider;
//             _updatable = updatable;
//             _inputService = inputService;
//             _eventListenerContainer = eventListenerContainer;
//         }
//
//         public ShipModel CreateShip<T, TT>(T firstWeapon, TT secondWeapon) where T : WeaponBase<Bullet> where TT : WeaponBase<Bullet>
//         {
//             var shipData = _assetProvider.GetData<ShipStaticData>(AssetPath.ShipPath);
//             var shipPrefab = Object.Instantiate(shipData.Prefab, Vector3.zero, Quaternion.identity);
//
//             var ship = new ShipModel(shipData.Acceleration, shipData.Deceleration, shipData.MaxSpeed, shipData.RotationSpeed,
//                 shipPrefab, _inputService, firstWeapon, secondWeapon);
//
//             var shipView = new ShipView(ship);
//             var shipPresenter = new ShipPresenter(ship, shipView);
//             
//             _eventListenerContainer.Register<IEventListener>(shipPresenter);
//
//             return ship;
//         }
//
//         public ScreenWrapper CreateWrapper(ShipModel shipModel)
//         {
//             var wrapper = new ScreenWrapper(_updatable, shipModel);
//             _eventListenerContainer.Register<IEventListener>(wrapper);
//             
//             Spawned?.Invoke(wrapper);
//
//             return wrapper;
//         }
//
//         public ProjectileWeapon CreateProjectileWeapon(GunType gunType)
//         {
//             GetStats(gunType, out var pool, out var weaponData);
//             var weapon = new ProjectileWeapon(pool, _updatable, gunType);
//
//             return weapon;
//         }
//
//         public LaserWeapon CreateLaserWeapon(GunType gunType)
//         {
//             GetStats(gunType, out var pool, out var weaponData);
//             var weapon = new LaserWeapon(pool, _updatable, gunType, weaponData.FireCooldown);
//             _eventListenerContainer.Register<IEventListener>(weapon);
//
//             return weapon;
//         }
//
//         public EnemySpawner CreateEnemySpawner(Transform prefabTransform)
//         {
//             var settings = _assetProvider.GetData<EnemySpawnerSettings>(AssetPath.EnemySpawnerSettings);
//             var enemySpawner = new EnemySpawner(prefabTransform, settings, CreateSpawnPointsContainer(), _updatable,
//                 CreateUfo, CreateAsteroid);
//             
//             _eventListenerContainer.Register<IEventListener>(enemySpawner);
//
//             return enemySpawner;
//         }
//         
//         private SpawnPointsContainer CreateSpawnPointsContainer()
//         {
//             var list = new List<ISpawnBehaviour>()
//             {
//                 new TopSpawnBehaviour(SpawnPoints.TopEdge),
//                 new BottomSpawnBehaviour(SpawnPoints.BottomEdge),
//                 new RightSpawnBehaviour(SpawnPoints.LeftEdge),
//                 new LeftSpawnBehaviour(SpawnPoints.RightEdge),
//             };
//
//             return new SpawnPointsContainer(list);
//         }
//
//         public Ufo CreateUfo(Transform playerShip)
//         {
//             var ufoData = _assetProvider.GetData<EnemyStaticData>(AssetPath.Ufo);
//             var asteroidPrefab = Object.Instantiate(ufoData.Prefab);
//             var ufo = new Ufo(asteroidPrefab, ufoData.Speed, playerShip, _updatable);
//             asteroidPrefab.gameObject.SetActive(false);
//             
//             _eventListenerContainer.Register<IEventListener>(ufo);
//             Spawned?.Invoke(ufo);
//
//             return ufo;
//         }
//
//         public Asteroid CreateAsteroid(Transform playerShip)
//         {
//             var asteroidData = _assetProvider.GetData<EnemyStaticData>(AssetPath.Asteroid);
//             var asteroidPrefab = Object.Instantiate(asteroidData.Prefab, Vector3.zero, Quaternion.identity);
//             var asteroid = new Asteroid(asteroidPrefab, asteroidData.Speed, _updatable, Camera.main);
//             asteroidPrefab.gameObject.SetActive(false);
//             
//             _eventListenerContainer.Register<IEventListener>(asteroid);
//             
//             Spawned?.Invoke(asteroid);
//
//             return asteroid;
//         }
//
//         private void GetStats(GunType gunType, out ObjectPool<Bullet> pool, out BulletStaticData weaponData)
//         {
//             var poolData = _assetProvider.GetData<PoolStaticData>(AssetPath.PoolPath);
//             weaponData = gunType == GunType.Projectile
//                 ? _assetProvider.GetData<BulletStaticData>(AssetPath.Projectile)
//                 : _assetProvider.GetData<BulletStaticData>(AssetPath.Laser);
//
//             pool = new ObjectPool<Bullet>(poolData.PoolSize, CreateBullet<Bullet>);
//         }
//
//         private T CreateBullet<T>(GunType gunType) where T : Bullet
//         {
//             var bulletData = gunType == GunType.Projectile
//                 ? _assetProvider.GetData<BulletStaticData>(AssetPath.Projectile)
//                 : _assetProvider.GetData<BulletStaticData>(AssetPath.Laser);
//
//             var bulletPrefab = Object.Instantiate(bulletData.Prefab);
//             bulletPrefab.SetActive(false);
//
//             var bullet = new Bullet(bulletData.Deceleration, _updatable, bulletPrefab, bulletData.GunType);
//             
//             var bulletView = new ProjectileView();
//             var bulletPresenter = new ProjectilePresenter(bullet, bulletView, _updatable);
//             //TODO: переделать presenter под MVP
//
//             _eventListenerContainer.Register<IEventListener>(bullet);
//             Spawned?.Invoke(bullet);
//
//             return bullet as T;
//         }
//     }
// }