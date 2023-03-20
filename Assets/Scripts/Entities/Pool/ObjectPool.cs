using System;
using Entities.Guns;

namespace Entities.Pool
{
    public class ObjectPool<T> : PoolBase<BulletType, T> where T : ITransformable
    {
        public event Action<T> Received;
        
        public ObjectPool(int poolSize, params Func<BulletType, T>[] createObject) : base(poolSize, createObject)
        {
        }
        
        public override T GetObject(BulletType bulletType)
        {
            var element = base.GetObject(bulletType);
            Received?.Invoke(element);

            return element;
        }
    }
}