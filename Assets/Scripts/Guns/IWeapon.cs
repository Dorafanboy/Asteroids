using System;
using UnityEngine;

namespace Guns
{
    public interface IWeapon<out T> where T : Bullet
    {
        event Action<T> Shooted;
        void Shoot(Vector3 position, Quaternion angle);
        Type GetBulletType();
    }
}