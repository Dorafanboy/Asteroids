using System;
using System.Collections.Generic;
using Entities.Guns;
using Entities.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities.Pool
{
    public class ObjectPool<T> where T : IPoolProduct
    {
        private readonly int _poolSize;
        private readonly Func<GunType, T>[] _createObject;
        private readonly Queue<T> _pool;
        private readonly ITest _test;

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
                    var idx = Random.Range(0, _createObject.Length);
                    var randomFunc = _createObject[idx];
                
                    _pool.Enqueue(randomFunc(gunType)); 
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

public interface ITest
{
    
}

// public abstract class PoolBase<T> where T : IPoolProduct
// {
//     private readonly FactoryBase _factoryBase;
//     private readonly int _poolSize;
//     private readonly Queue<T> _pool;
//
//     protected PoolBase(FactoryBase factoryBase, int poolSize)
//     {
//         _factoryBase = factoryBase;
//         _poolSize = poolSize;
//         _pool = new Queue<T>();
//     }
//     
//     public T GetObject(GunType gunType)
//     {
//         if (_pool.Count <= 0)
//         {
//             for (int i = 0; i < _poolSize; i++)
//             {
//                 var entity = _factoryBase
//                 _pool.Enqueue(entity);
//             }
//         }
//     
//         return _pool.Dequeue();
//     }
//         
//     public void ReturnObject(T obj)
//     {
//         _pool.Enqueue(obj);
//     }
// }

public class EnemyObjectPool<T> where T : IPoolProduct
{
    private readonly int _poolSize;
    private readonly Func<Transform, T>[] _createObject;
    private readonly Queue<T> _pool;

    public EnemyObjectPool(int poolSize, params Func<Transform, T>[] createObject)
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
                var idx = Random.Range(0, _createObject.Length);
                var randomFunc = _createObject[idx];
                
                _pool.Enqueue(randomFunc(transform));
            }
        }

        return _pool.Dequeue();
    }
        
    public void ReturnObject(T obj)
    {
        _pool.Enqueue(obj);
    }
}