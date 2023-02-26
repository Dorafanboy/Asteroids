using Guns;
using Infrastructure.Wrapper;
using ShipContent;

namespace Infrastructure.Services.Factories
{
    public interface IFactory : IService
    {
        Ship CreateShip<T, TT>(T firstWeapon, TT secondWeapon) where T : Weapon<Bullet> where TT : Weapon<Bullet>;
        ScreenWrapper CreateWrapper(Ship ship);
        ProjectileWeapon CreateProjectileWeapon(GunType gunType);
        LaserWeapon CreateLaserWeapon(GunType gunType);
    }
}