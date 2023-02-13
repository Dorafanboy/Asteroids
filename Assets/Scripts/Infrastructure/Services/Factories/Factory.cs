using System.Collections.Generic;
using Guns;
using UnityEngine;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Inputs;
using Infrastructure.Wrapper;
using ShipContent;
using StaticData;

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

        public Ship CreateShip()
        {
            var shipData = _assetProvider.GetData<ShipStaticData>(AssetPath.ShipPath);
            var shipPrefab = Object.Instantiate(shipData.Prefab, Vector3.zero, Quaternion.identity);

            var weapon = CreateWeapon(CreateProjectile().Type, AssetPath.BulletPath, AssetPath.PoolPath);
            
            var ship = new Ship(shipData.Acceleration, shipData.Deceleration, shipData.MaxSpeed, 
                shipData.RotationSpeed, shipData.ShotCooldown, shipData.MaxAmmo, shipPrefab, _inputService, weapon);
            
            var shipView = new ShipView(ship);
            var shipPresenter = new ShipPresenter(ship, shipView);
            
            return ship;
        }

        public ScreenWrapper CreateWrapper(Ship ship)
        {
            var wrapper = new ScreenWrapper(_updatable, ship);
            wrapper.Enable();

            return wrapper;
        }

        public IWeapon CreateWeapon(GunType gunType, string bulletPath, string poolPath)
        {
            var poolData = _assetProvider.GetData<PoolStaticData>(poolPath);
            var pool = new ObjectPool<Projectile>(poolData.PoolSize, CreateQueue);

            var weapon = new Weapon(gunType, pool);

            var screen = new BulletScreenWrapper(_updatable, pool, weapon);

            return weapon;
        }

        private Projectile CreateProjectile()
        {
            var bulletData = _assetProvider.GetData<BulletStaticData>(AssetPath.BulletPath);
            
            var bulletPrefab = Object.Instantiate(bulletData.Prefab);
            bulletPrefab.SetActive(false);

            var bullet = new Projectile(bulletPrefab, bulletData.GunType, bulletData.Deceleration, _updatable);
            var bulletView = new ProjectileView();
            var bulletPresenter = new ProjectilePresenter(bullet, bulletView, _updatable);

            return bullet;
        }

        private Projectile CreateQueue() 
        {
            var bullet = CreateProjectile();

            return bullet;
        }
    }
}