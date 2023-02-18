using Guns;
using Infrastructure.Wrapper;
using ShipContent;

namespace Infrastructure.Services.Factories
{
    public interface IFactory : IService
    {
        Ship CreateShip<T, TT>(T firstWeapon, TT secondWeapon) where T : IWeapon<Bullet> where TT : IWeapon<Bullet>;
        ScreenWrapper CreateWrapper(Ship ship);
        IWeapon<T> CreateWeapon<T>(GunType gunType) where T : Bullet;
    }
}