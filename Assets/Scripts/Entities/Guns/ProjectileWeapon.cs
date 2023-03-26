using Entities.Pool;

namespace Entities.Guns
{
    public class ProjectileWeapon : WeaponBase<Bullet>
    {
        public ProjectileWeapon(BulletObjectPool<Bullet> bulletObjectPool, BulletType bulletType) : base(bulletObjectPool, bulletType)
        {
        }
    }
}