using System;
using System.Collections.Generic;

namespace Guns
{
    public class ObjectPool<T> where T : IPoolProduct
    {
        private readonly int _poolSize;
        private readonly Func<GunType, T> _createObject;
        private readonly Queue<T> _pool;

        public ObjectPool(int poolSize, Func<GunType, T> createObject)
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
                    _pool.Enqueue(_createObject(gunType));
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