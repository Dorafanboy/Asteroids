using Infrastructure.Services.Factories;
using UnityEngine;

namespace Guns
{
    public interface IWeapon
    {
        IFactory Factory { get; }
        void Shoot(Vector3 position, Quaternion angle);
    }
}