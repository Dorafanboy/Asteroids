using System.Collections.Generic;
using Constants;
using Entities.Guns;
using Entities.Pool;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Containers;
using StaticData;
using UnityEngine;

namespace Infrastructure.Services.Factories
{
    public class WeaponFactory : FactoryBase
    {
        private readonly IUpdatable _updatable;
        private readonly Camera _camera;
        private readonly Dictionary<GunType, BulletStaticData> _factories;

        public WeaponFactory(IAssetProvider assetProvider, EventListenerContainer eventListenerContainer,
            IUpdatable updatable, Camera camera) : base(assetProvider, eventListenerContainer)
        {
            _updatable = updatable;
            _camera = camera;

            _factories = new Dictionary<GunType, BulletStaticData>
            {
                { GunType.Laser, AssetProvider.GetData<BulletStaticData>(AssetPath.Laser) },
                { GunType.Projectile, AssetProvider.GetData<BulletStaticData>(AssetPath.Projectile) },
            };
        }

        public ProjectileWeapon CreateProjectileWeapon(GunType gunType)
        {
            GetStats(out var pool, out _, AssetPath.Projectile);
            var weapon = new ProjectileWeapon(pool, _updatable, gunType, _camera);
        
            return weapon;
        }

        public LaserWeapon CreateLaserWeapon(GunType gunType)
        {
            GetStats(out var pool, out var weaponData, AssetPath.Laser);
            var weapon = new LaserWeapon(pool, _updatable, gunType, weaponData.FireCooldown, _camera);
            
            EventListenerContainer.Register<IEventListener>(weapon);
        
            return weapon;
        }

        private void GetStats(out ObjectPool<Bullet> pool, out BulletStaticData weaponData, string path) //TODO: гантайп не нужен будет
        {
            var poolData = AssetProvider.GetData<PoolStaticData>(AssetPath.PoolPath);
            weaponData = AssetProvider.GetData<BulletStaticData>(path);
        
            pool = new ObjectPool<Bullet>(poolData.PoolSize, CreateBullet<Bullet>);
        }

        private T CreateBullet<T>(GunType gunType) where T : Bullet         //TODO: переделать presenter под MVP
        {
            var bulletData = _factories[gunType];

            var bulletPrefab = Object.Instantiate(bulletData.Prefab);
            bulletPrefab.SetActive(false);

            var bullet = new Bullet(bulletData.Deceleration, _updatable, bulletPrefab);
            
            var bulletView = new ProjectileView();
            var bulletPresenter = new ProjectilePresenter(bullet, bulletView, _updatable);

            EventListenerContainer.Register<IEventListener>(bullet);

            return bullet as T;
        }
    }
}