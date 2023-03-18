using System;
using System.Collections.Generic;
using Entities.Guns;
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

            var element = _pool.Dequeue();
            element.Collided += OnCollided;

            return element;
        }

        public void ReturnObject(T obj)
        {
            obj.Collided -= OnCollided;
            _pool.Enqueue(obj);
        }

        private void OnCollided(ITransformable obj)
        {
            obj.Prefab.SetActive(false);
            _pool.Enqueue((T)obj);
        }
    }
}