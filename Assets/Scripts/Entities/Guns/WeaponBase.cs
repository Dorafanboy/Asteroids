using System;
using Entities.Pool;
using UnityEngine;

namespace Entities.Guns
{
    public abstract class WeaponBase<T> : IWeapon<T> where T : Bullet
    {
        private readonly BulletObjectPool<T> _bulletObjectPool;
        private readonly BulletType _bulletType;
        public event Action<T> Fired;
        
        protected WeaponBase(BulletObjectPool<T> bulletObjectPool, BulletType bulletType)
        {
            _bulletObjectPool = bulletObjectPool;
            _bulletType = bulletType;
        }

        public virtual void Shoot(Vector3 position, Quaternion angle)
        {
            var bullet = _bulletObjectPool.GetObject(_bulletType);
                
            bullet.Prefab.transform.position = position;
            bullet.Prefab.transform.eulerAngles = angle.eulerAngles;
            bullet.Prefab.SetActive(true);

            Fired?.Invoke(bullet);
        }
    }
}
