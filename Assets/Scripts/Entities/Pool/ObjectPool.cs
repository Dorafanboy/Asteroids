using System;
using Entities.Guns;

namespace Entities.Pool
{
    public class ObjectPool<T> : PoolBase<GunType, T> where T : ITransformable
    {
        public event Action<T> Received;
        
        public ObjectPool(int poolSize, params Func<GunType, T>[] createObject) : base(poolSize, createObject)
        {
        }
        
        public override T GetObject(GunType gunType)
        {
            var element = base.GetObject(gunType);
            Received?.Invoke(element);

            return element;
        }
    }
}