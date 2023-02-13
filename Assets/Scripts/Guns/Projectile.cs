using System;
using Infrastructure;
using UnityEngine;

namespace Guns
{
    public class Projectile : IPoolProduct, IUpdateListener
    {
        public GameObject Prefab { get; }
        public GunType Type { get; }
        public float Deceleration { get; }
        public IUpdatable Updatable;

        public Projectile(GameObject prefab, GunType type, float deceleration, IUpdatable updatable)
        {
            Prefab = prefab;
            Type = type;
            Deceleration = deceleration;
            Updatable = updatable;
            
            Enable();
        }
        
        public void Enable()
        {
            Updatable.Updated += OnUpdated;
        }

        public void Disable()
        {
            Updatable.Updated -= OnUpdated;
        }

        public void OnUpdated(float time)
        {
            Prefab.transform.position += time * Prefab.transform.up * Deceleration;
        }
    }
}