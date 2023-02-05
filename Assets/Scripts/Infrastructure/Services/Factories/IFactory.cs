using Guns;
using Infrastructure.Wrapper;
using ShipContent;
using UnityEngine;

namespace Infrastructure.Services.Factories
{
    public interface IFactory : IService
    {
        Ship CreateShip();
        ScreenWrapper CreateWrapper(Ship ship);
        Projectile CreateProjectile(Vector3 position, Quaternion angle, GunType gunType);
    }
}