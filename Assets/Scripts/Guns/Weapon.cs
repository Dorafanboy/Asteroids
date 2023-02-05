using Infrastructure.Services.Factories;
using UnityEngine;

namespace Guns
{
    public class Weapon : IWeapon
    {
        public IFactory Factory { get; }
        public GunType GunType { get; }

        public Weapon(IFactory factory, GunType gunType)
        {
            Factory = factory;
            GunType = gunType;
        }

        public void Shoot(Vector3 position, Quaternion angle)
        {
            Factory.CreateProjectile(position, angle, GunType);
        }
    }
}