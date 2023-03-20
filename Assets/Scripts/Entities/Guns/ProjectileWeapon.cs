using Entities.Pool;

namespace Entities.Guns
{
    public class ProjectileWeapon : WeaponBase<Bullet>
    {
        public ProjectileWeapon(ObjectPool<Bullet> objectPool, BulletType bulletType) : base(objectPool, bulletType)
        {
        }
    }
}