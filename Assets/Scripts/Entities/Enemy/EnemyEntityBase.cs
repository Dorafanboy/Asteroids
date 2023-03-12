﻿using Entities.Pool;
using Infrastructure;
using UnityEngine;

namespace Entities.Enemy
{
    public abstract class EnemyEntityBase : IPoolProduct, IUpdateListener
    {
        public GameObject Prefab { get; }
        protected float Speed { get; }
        
        private readonly IUpdatable _updatable;

        protected EnemyEntityBase(GameObject prefab, float speed, IUpdatable updatable)
        {
            Prefab = prefab;
            Speed = speed;
            _updatable = updatable;
        }

        public void Enable()
        {
            _updatable.Updated += OnUpdated;
        }

        public void Disable()
        {
            _updatable.Updated -= OnUpdated;
        }

        public abstract void OnUpdated(float time);
    }
}
//TODO Сделать обработку коллизий
