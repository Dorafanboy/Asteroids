using System;
using System.Collections.Generic;
using System.Linq;
using Entities.Guns;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities.Guns
{
    public class ObjectPool<T> where T : IPoolProduct
    {
        private readonly int _poolSize;
        private readonly Func<GunType, T>[] _createObject;
        private readonly Queue<T> _pool;

        public ObjectPool(int poolSize, params Func<GunType, T>[] createObject)
        {
            _poolSize = poolSize;
            _createObject = createObject;
            _pool = new Queue<T>();
        }

        public T GetObject(GunType gunType)
        {
            if (_pool.Count <= 0)
            {
                for (int i = 0; i < _poolSize; i++)
                {
                    if (_createObject.Length > 0)
                    {
                        var randomFunc = _createObject.First();
                        _pool.Enqueue(randomFunc(gunType));
                    }
                }
            }

            return _pool.Dequeue();
        }
        
        public void ReturnObject(T obj)
        {
            _pool.Enqueue(obj);
        }
    }
}

public class AsteroidObjectPool<T> where T : IPoolProduct
{
    private readonly int _poolSize;
    private readonly Func<Transform, T>[] _createObject;
    private readonly Queue<T> _pool;

    public AsteroidObjectPool(int poolSize, params Func<Transform, T>[] createObject)
    {
        _poolSize = poolSize;
        _createObject = createObject;
        _pool = new Queue<T>();
    }

    public T GetObject(Transform transform)
    {
        if (_pool.Count <= 0)
        {
            for (int i = 0; i < _poolSize; i++)
            {
                if (_createObject.Length > 0)
                {
                    var idx = Random.Range(0, _createObject.Length);
                    var randomFunc = _createObject[idx];
                    
                    _pool.Enqueue(randomFunc(transform));
                }
            }
        }

        return _pool.Dequeue();
    }
        
    public void ReturnObject(T obj)
    {
        _pool.Enqueue(obj);
    }
}
