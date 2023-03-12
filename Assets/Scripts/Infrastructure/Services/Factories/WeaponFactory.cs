using Constants;
using Entities.Guns;
using Entities.Pool;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Containers;
using StaticData;
using UnityEngine;

namespace Infrastructure.Services.Factories
{
    public class WeaponFactory
    {
        private readonly EventListenerContainer _eventListenerContainer;
        private readonly IUpdatable _updatable;
        private readonly AssetProvider _assetProvider;

        public WeaponFactory(EventListenerContainer eventListenerContainer, IUpdatable updatable, AssetProvider assetProvider)
        {
            _eventListenerContainer = eventListenerContainer;
            _updatable = updatable;
            _assetProvider = assetProvider;
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
            _eventListenerContainer.Register<IEventListener>(weapon);
        
            return weapon;
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
            //TODO: переделать presenter под MVP

            _eventListenerContainer.Register<IEventListener>(bullet);

            return bullet as T;
        }
    }
}