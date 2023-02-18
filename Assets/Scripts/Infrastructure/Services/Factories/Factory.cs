using Guns;
using UnityEngine;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Inputs;
using Infrastructure.Wrapper;
using ShipContent;
using StaticData;
using Bullet = Guns.Bullet;

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

        public Ship CreateShip<T, TT>(T firstWeapon, TT secondWeapon) where T : IWeapon<Bullet> where TT : IWeapon<Bullet>
        {
            var shipData = _assetProvider.GetData<ShipStaticData>(AssetPath.ShipPath);
            var shipPrefab = Object.Instantiate(shipData.Prefab, Vector3.zero, Quaternion.identity);

            var ship = new Ship(shipData.Acceleration, shipData.Deceleration, shipData.MaxSpeed, 
                shipData.RotationSpeed, shipData.ShotCooldown, shipData.MaxAmmo, shipPrefab,
                _inputService, firstWeapon, secondWeapon);

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

        public IWeapon<T> CreateWeapon<T>(GunType gunType) where T : Bullet
        {
            var weapon = new Weapon<T>(CreateBullet<T>, _updatable, gunType);
            
            return weapon;  
        }
        
        private T CreateBullet<T>(GunType gunType) where T : Bullet 
        {
            var bulletData = gunType == GunType.Projectile
                ? _assetProvider.GetData<BulletStaticData>(AssetPath.Projectile)
                : _assetProvider.GetData<BulletStaticData>(AssetPath.Laser);
            
            var bulletPrefab = Object.Instantiate(bulletData.Prefab);
            
            var bullet = new Bullet(bulletData.Deceleration, _updatable, bulletPrefab, bulletData.GunType);
            var bulletView = new ProjectileView();
            var bulletPresenter = new ProjectilePresenter(bullet, bulletView, _updatable);

            return bullet as T;
        }

        public Bullet CreateBullet(IWeapon<Bullet> weapon)
        {
            var bulletData = weapon.GetBulletType() == typeof(Projectile)
                ? _assetProvider.GetData<BulletStaticData>(AssetPath.Projectile)
                : _assetProvider.GetData<BulletStaticData>(AssetPath.Laser);
            
            var bulletPrefab = Object.Instantiate(bulletData.Prefab);
            bulletPrefab.SetActive(false);
            
            var bullet = new Bullet(bulletData.Deceleration, _updatable, bulletPrefab, bulletData.GunType);
            var bulletView = new ProjectileView();
            var bulletPresenter = new ProjectilePresenter(bullet, bulletView, _updatable);

            return bullet;
        }
    }
}