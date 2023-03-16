﻿using System;
using System.Collections.Generic;
using Entities.Guns;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities.Pool
{
    public abstract class PoolBase<TT, T> where T : ITransformable
    {
        private readonly int _poolSize;
        private readonly Func<TT, T>[] _createObject;
        private readonly Queue<T> _pool;

        protected PoolBase(int poolSize, params Func<TT, T>[] createObject)
        {
            _poolSize = poolSize;
            _createObject = createObject;
            _pool = new Queue<T>();
        }

        public virtual T GetObject(TT type)
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
    
            return _pool.Dequeue();
        }
        
        public void ReturnObject(T obj)
        {
            _pool.Enqueue(obj);
        }
    }
}