using System;
using UnityEngine;

namespace Guns
{
    public class Projectile
    {
        public GameObject Prefab { get; }
        public GunType Type { get; }
        public float Deceleration { get; }

        public Projectile(GameObject prefab, GunType type, float deceleration)
        {
            Prefab = prefab;
            Type = type;
            Deceleration = deceleration;
        }
    }
}