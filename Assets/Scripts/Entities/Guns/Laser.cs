using Infrastructure;
using UnityEngine;

namespace Entities.Guns
{
    public class Laser : Bullet
    {
        public Laser(float deceleration, IUpdatable updatable, GameObject prefab, GunType type) : base(deceleration, updatable, prefab, type)
        {
        }
    }
}