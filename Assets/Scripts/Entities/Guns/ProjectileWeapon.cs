using Entities.Pool;
using Infrastructure;
using UnityEngine;

namespace Entities.Guns
{
    public class ProjectileWeapon : WeaponBase<Bullet>
    {
        public ProjectileWeapon(ObjectPool<Bullet> objectPool, IUpdatable updatable, GunType gunType, Camera camera) 
            : base(objectPool, updatable, gunType, camera)
        {
        }
    }
}