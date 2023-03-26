using System;
using Entities.Guns;
using Infrastructure;
using Infrastructure.Services.Clashes;
using UnityEngine;

namespace Entities.Enemy
{
    public abstract class EnemyEntityBase : ITransformable, IUpdateListener
    {
        public GameObject Prefab { get; }
        protected float Speed { get; }
        private readonly IUpdatable _updatable;
        public CollisionType CollisionType { get; }

        public event Action<ITransformable> Collided;

        protected EnemyEntityBase(GameObject prefab, float speed, IUpdatable updatable, CollisionType collisionType)
        {
            Prefab = prefab;
            Speed = speed;
            _updatable = updatable;
            CollisionType = collisionType;
        }

        public void Enable()
        {
            _updatable.Updated += OnUpdated;
        }

        public void Disable()
        {
            _updatable.Updated -= OnUpdated;
        }

        public void DisableObject()
        {
            Collided?.Invoke(this);
        }

        public void InstallPosition(Vector3 position)
        {
            Prefab.transform.position = position;
        }

        public abstract void OnUpdated(float time);
    }
}