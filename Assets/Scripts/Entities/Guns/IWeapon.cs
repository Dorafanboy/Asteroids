using System;
using UnityEngine;

namespace Entities.Guns
{
    public interface IWeapon<out T> where T : Bullet
    {
        event Action<T> Fired;
        void Shoot(Vector3 position, Quaternion angle);
    }
}