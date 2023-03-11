using Entities.Pool;
using Infrastructure;

namespace Entities.Guns
{
    public class ProjectileWeapon : Weapon<Bullet>
    {
        public ProjectileWeapon(ObjectPool<Bullet> objectPool, IUpdatable updatable, GunType gunType, float bulletCooldown) 
            : base(objectPool, updatable, gunType)
        {
        }
    }
}