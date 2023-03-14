using System;
using Entities.Pool;
using UnityEngine;

namespace Entities.Guns
{
    public abstract class WeaponBase<T> : IWeapon<T> where T : Bullet
    {
        private readonly ObjectPool<T> _objectPool;
        private readonly GunType _gunType;
        public event Action<T> Fired;
        
        protected WeaponBase(ObjectPool<T> objectPool, GunType gunType)
        {
            _objectPool = objectPool;
            _gunType = gunType;
        }

        public virtual void Shoot(Vector3 position, Quaternion angle)
        {
            var bullet = _objectPool.GetObject(_gunType);
                
            bullet.Prefab.transform.position = position;
            bullet.Prefab.transform.eulerAngles = angle.eulerAngles;
            bullet.Prefab.SetActive(true);

            Fired?.Invoke(bullet);
        }
    }
}
