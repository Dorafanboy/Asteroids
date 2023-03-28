using System;
using System.Collections.Generic;
using System.Linq;
using Entities.Guns;
using Entities.Pool;
using Infrastructure;
using Infrastructure.Services.Clashes;
using UnityEngine;

namespace Entities.Pool
{
    public class EnemyObjectPool<T> : PoolBase<Transform, T> where T : ITransformable
    {
        public event Action<T> Received;

        public EnemyObjectPool(int poolSize, params Func<Transform, T>[] createObject) : base(poolSize, createObject)
        {
        }

        public override T GetObject(Transform type)
        {
            var element = base.GetObject(type);
            Received?.Invoke(element);

            return element;
        }
    }
}

public class EnemyCollisionHandler<T> : IEventListener where T : ITransformable
{
    private readonly EnemyObjectPool<T> _enemyPool;
    private readonly CollisionHandler _collisionHandler;
    private readonly Dictionary<CollisionChecker, T> _enemies;

    public event Action<T> AsteroidDestroyedByProjectile;

    public EnemyCollisionHandler(EnemyObjectPool<T> enemyPool, CollisionHandler collisionHandler)
    {
        _enemyPool = enemyPool;
        _collisionHandler = collisionHandler;
        _enemies = new Dictionary<CollisionChecker, T>();

        Enable();
    }

    public void Enable()
    {
        _enemyPool.Received += OnReceived;
        _collisionHandler.UfoDestroyedByLaser += OnUfoDestroyedByLaser;
    }

    public void Disable()
    {
        _enemyPool.Received -= OnReceived;
        _collisionHandler.UfoDestroyedByLaser -= OnUfoDestroyedByLaser;
    }

    private void OnReceived(T obj)
    {
        var checker = obj.Prefab.GetComponent<CollisionChecker>();
        _enemies.Add(checker, obj);
    }

    private void OnUfoDestroyedByLaser(CollisionChecker obj, bool diedFromProjectile)
    {
        if (_enemies.ContainsKey(obj) == false)
        {
            return;
        }
        
        var trans = _enemies[obj];
        
        if (diedFromProjectile && obj.CollisionType == CollisionType.Asteroid)
        {
            AsteroidDestroyedByProjectile?.Invoke(trans);
            _enemyPool.ReturnObject(trans);
        }

        _enemies.Remove(obj);
    }
}