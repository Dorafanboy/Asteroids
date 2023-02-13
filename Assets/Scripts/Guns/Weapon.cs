using System;
using UnityEngine;

namespace Guns
{
    public class Weapon : IWeapon
    {
        public GunType GunType { get; }
        
        private readonly ObjectPool<Projectile> _objectPool;
        
        public event Action<Projectile> Shooted;

        public Weapon(GunType gunType, ObjectPool<Projectile> objectPool)
        {
            GunType = gunType;
            _objectPool = objectPool;
        }

        public void Shoot(Vector3 position, Quaternion angle)
        {
            var bullet = _objectPool.GetObject();

            bullet.Prefab.transform.position = position;
            bullet.Prefab.transform.eulerAngles = angle.eulerAngles;
            bullet.Prefab.SetActive(true);
            
            Shooted?.Invoke(bullet);
        }
    }
}