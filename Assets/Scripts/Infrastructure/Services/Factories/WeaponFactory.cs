using System.Collections.Generic;
using Constants;
using Entities.Guns;
using Entities.Pool;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Clashes;
using Infrastructure.Services.Containers;
using Infrastructure.Wrapper;
using StaticData;
using UnityEngine;

namespace Infrastructure.Services.Factories
{
    public class WeaponFactory : FactoryBase
    {
        private readonly IUpdatable _updatable;
        private readonly Camera _camera;
        private readonly Dictionary<BulletType, BulletStaticData> _factories;

        public WeaponFactory(IAssetProvider assetProvider, EventListenerContainer eventListenerContainer,
            IUpdatable updatable, Camera camera, TransformableContainer transformableContainer) 
            : base(assetProvider, eventListenerContainer, transformableContainer)
        {
            _updatable = updatable;
            _camera = camera;

            _factories = new Dictionary<BulletType, BulletStaticData>
            {
                { BulletType.Laser, AssetProvider.GetData<BulletStaticData>(AssetPath.Laser) },
                { BulletType.Projectile, AssetProvider.GetData<BulletStaticData>(AssetPath.Projectile) },
            };
        }

        public ProjectileWeapon CreateProjectileWeapon(BulletType bulletType)
        {
            GetStats(out var pool, out _, AssetPath.Projectile);
            var weapon = new ProjectileWeapon(pool, bulletType);
            
            return weapon;
        }

        public LaserWeapon CreateLaserWeapon(BulletType bulletType)
        {
            GetStats(out var pool, out var weaponData, AssetPath.Laser);
            var weapon = new LaserWeapon(pool, _updatable, bulletType, weaponData.FireCooldown);
            
            EventListenerContainer.Register<IEventListener>(weapon);
        
            return weapon;
        }

        private void GetStats(out BulletObjectPool<Bullet> pool, out BulletStaticData weaponData, string path)
        {
            var poolData = AssetProvider.GetData<PoolStaticData>(AssetPath.PoolPath);
            weaponData = AssetProvider.GetData<BulletStaticData>(path);
        
            pool = new BulletObjectPool<Bullet>(poolData.PoolSize, CreateBullet<Bullet>);           
            var wrapper = new BulletScreenWrapper<Bullet>(_updatable, _camera, pool);
            
            EventListenerContainer.Register<IEventListener>(wrapper);
        }

        private T CreateBullet<T>(BulletType bulletType) where T : Bullet  //TODO: переделать presenter под MVP
        {
            var bulletData = _factories[bulletType];

            var bulletPrefab = Object.Instantiate(bulletData.Prefab);
            bulletPrefab.SetActive(false);

            var bullet = new Bullet(bulletData.Deceleration, _updatable, bulletPrefab, (CollisionType)bulletType);
            
            var bulletView = new ProjectileView();
            var bulletPresenter = new ProjectilePresenter(bullet, bulletView, _updatable);

            EventListenerContainer.Register<IEventListener>(bullet);
            
            TransformableContainer.RegisterObject(bullet.Prefab.GetComponent<CollisionChecker>());

            return bullet as T;
        }
    }
}

public struct BulletDataType
{
    public BulletType BulletType { get; }
    public BulletStaticData StaticData { get; }
    public CollisionType CollisionType { get; }

    public BulletDataType(BulletType bulletType, BulletStaticData staticData, CollisionType collisionType)
    {
        BulletType = bulletType;
        StaticData = staticData;
        CollisionType = collisionType;
    }
}
