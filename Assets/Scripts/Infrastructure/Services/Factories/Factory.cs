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

        public Ship CreateShip<T, TT>(T firstWeapon, TT secondWeapon) where T : Weapon<Bullet> where TT : Weapon<Bullet>
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

        private void GetStats(GunType gunType, out ObjectPool<Bullet> pool, out BulletStaticData weaponData, out int shotCount)
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
