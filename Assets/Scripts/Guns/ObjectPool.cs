using System;
using System.Collections.Generic;

namespace Guns
{
    public class ObjectPool<T> where T : IPoolProduct
    {
        private readonly int _poolSize;
        private readonly Func<T> _createObject;
        private readonly Queue<T> _pool;

        public ObjectPool(int poolSize, Func<T> createObject)
        {
            _poolSize = poolSize;
            _createObject = createObject;
            _pool = new Queue<T>();
            
            for (int i = 0; i < _poolSize; i++)
            {
                _pool.Enqueue(_createObject());
            }
        }

        public T GetObject()
        {
            if (_pool.Count <= 0)
            {
                for (int i = 0; i < _poolSize; i++)
                {
                    _pool.Enqueue(_createObject());
                }
            }

            var element = _pool.Dequeue();
                        
            return element;
        }
        
        public void ReturnObject(T obj)
        {
            _pool.Enqueue(obj);
        }
    }
}