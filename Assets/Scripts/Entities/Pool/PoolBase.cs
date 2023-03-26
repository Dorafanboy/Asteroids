using System;
using System.Collections.Generic;
using System.Linq;
using Entities.Guns;
using Infrastructure.Services.Clashes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities.Pool
{
    public abstract class PoolBase<TT, T> where T : ITransformable
    {
        private readonly int _poolSize;
        private readonly Func<TT, T>[] _createObject;
        private Queue<T> _pool;

        protected PoolBase(int poolSize, params Func<TT, T>[] createObject)
        {
            _poolSize = poolSize;
            _createObject = createObject;
            _pool = new Queue<T>();
        }

        public virtual T GetObject(TT type)
        {
            FillPool(type);
            var element = _pool.Dequeue();

            return element;
        }
        
        public T GetObjectByPrefab(CollisionType collisionType, TT type)
        {
            FillPool(type);
            var element = _pool
                .FirstOrDefault(e => e.CollisionType == collisionType);
            
            _pool = new Queue<T>(_pool
                .Where(ex => element != null && ex.CollisionType != element.CollisionType));

            return element;
        }

        public void ReturnObject(T obj)
        {
            _pool.Enqueue(obj);
            obj.Prefab.SetActive(false);
        }

        private void FillPool(TT type)
        {
            if (_pool.Count <= 0)
            {
                for (int i = 0; i < _poolSize; i++)
                {
                    var idx = Random.Range(0, _createObject.Length);
                    var randomFunc = _createObject[idx];

                    _pool.Enqueue(randomFunc(type));
                }
            }
        }
    }
}