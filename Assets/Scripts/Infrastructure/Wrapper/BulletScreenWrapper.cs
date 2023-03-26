using System.Collections.Generic;
using System.Linq;
using Entities.Guns;
using Entities.Pool;
using UnityEngine;

namespace Infrastructure.Wrapper
{
    public class BulletScreenWrapper<T> : WrapperBase where T : Bullet
    {
        private readonly BulletObjectPool<T> _bulletObjectPool;
        private readonly List<T> _bullets;

        public BulletScreenWrapper(IUpdatable updatable, Camera camera, BulletObjectPool<T> bulletObjectPool) 
            : base(updatable, camera)
        {
            _bulletObjectPool = bulletObjectPool;
            _bullets = new List<T>();
        }

        public override void Enable()
        {
            base.Enable();
            _bulletObjectPool.Received += OnReceived;
        }

        public override void Disable()
        {
            base.Disable();
            _bulletObjectPool.Received -= OnReceived;
        }

        public override void OnUpdated(float time)
        {
            for (int i = 0; i < _bullets.ToList().Count; i++)
            {
                if (IsNeedWrap(_bullets[i]))
                {
                    _bulletObjectPool.ReturnObject(_bullets[i]);
                    _bullets.Remove(_bullets[i]);
                }
            }
        }

        private void OnReceived(T poolObject)
        {
            _bullets.Add(poolObject);
        }
    }
}