using Infrastructure;
using UnityEngine;

namespace Entities.Guns
{
    public class Projectile : Bullet
    {
        public Projectile(float deceleration, IUpdatable updatable, GameObject prefab, GunType type) : base(deceleration, updatable, prefab)
        {
        }
    }
}