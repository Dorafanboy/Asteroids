using System;
using Entities.Pool;
using UnityEngine;

namespace Entities.Guns
{
    public abstract class WeaponBase<T> : IWeapon<T> where T : Bullet
    {
        private readonly ObjectPool<T> _objectPool;
        private readonly BulletType _bulletType;
        public event Action<T> Fired;
        
        protected WeaponBase(ObjectPool<T> objectPool, BulletType bulletType)
        {
            _objectPool = objectPool;
            _bulletType = bulletType;
        }

        public virtual void Shoot(Vector3 position, Quaternion angle)
        {
            var bullet = _objectPool.GetObject(_bulletType);
                
            bullet.Prefab.transform.position = position;
            bullet.Prefab.transform.eulerAngles = angle.eulerAngles;
            bullet.Prefab.SetActive(true);

            Fired?.Invoke(bullet);
        }
    }
}
