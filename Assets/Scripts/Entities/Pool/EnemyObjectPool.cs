using System;
using Entities.Guns;
using UnityEngine;

namespace Entities.Pool
{
    public class EnemyObjectPool<T> : PoolBase<Transform, T> where T : ITransformable
    {
        public EnemyObjectPool(int poolSize, params Func<Transform, T>[] createObject) : base(poolSize, createObject)
        {
        }
    }
}