using UnityEngine;

namespace Guns
{
    public interface IWeapon
    {
        void Shoot(Vector3 position, Quaternion angle);
    }
}