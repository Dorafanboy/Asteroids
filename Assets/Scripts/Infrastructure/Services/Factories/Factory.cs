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

            var bulletData = _assetProvider.GetData<BulletStaticData>(AssetPath.BulletPath);
            var weapon = new Weapon(this, bulletData.GunType);
            
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

        public Projectile CreateProjectile(Vector3 position, Quaternion angle, GunType gunType)
        {
            var bulletData = _assetProvider.GetData<BulletStaticData>(AssetPath.BulletPath);
            var bulletPrefab = Object.Instantiate(bulletData.Prefab, position, angle);

            var bullet = new Projectile(bulletPrefab, gunType, bulletData.Deceleration);
            var bulletView = new ProjectileView();
            var bulletPresenter = new ProjectilePresenter(bullet, bulletView, _updatable);

            return bullet;
        }
    }
}