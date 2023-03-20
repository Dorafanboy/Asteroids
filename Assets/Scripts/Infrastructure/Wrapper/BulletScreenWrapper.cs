using System.Collections.Generic;
using System.Linq;
using Entities.Guns;
using Entities.Pool;
using UnityEngine;

namespace Infrastructure.Wrapper
{
    public class BulletScreenWrapper<T> : WrapperBase where T : Bullet
    {
        private readonly ObjectPool<T> _objectPool;
        private readonly List<T> _bullets;

        public BulletScreenWrapper(IUpdatable updatable, Camera camera, ObjectPool<T> objectPool) 
            : base(updatable, camera)
        {
            _objectPool = objectPool;
            _bullets = new List<T>();
        }

        public override void Enable()
        {
            base.Enable();
            _objectPool.Received += OnReceived;
        }

        public override void Disable()
        {
            base.Disable();
            _objectPool.Received -= OnReceived;
        }

        public override void OnUpdated(float time)
        {
            for (int i = 0; i < _bullets.ToList().Count; i++)
            {
                if (IsNeedWrap(_bullets[i]))
                {
                    _bullets[i].Prefab.SetActive(false);

                    _objectPool.ReturnObject(_bullets[i]);

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