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

        public BulletScreenWrapper(IUpdatable updatable, Camera camera, ObjectPool<T> objectPool) : base(updatable, camera)
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
            _objectPool.Received -= OnReceived;
            base.Disable();
        }

        public override void OnUpdated(float time)
        {
            foreach (var bullet in _bullets.ToList())
            {
                if (IsNeedReturn(bullet))
                {
                    bullet.Prefab.SetActive(false);

                    _objectPool.ReturnObject(bullet);

                    _bullets.Remove(bullet);
                }
            }
        }

        private bool IsNeedReturn(Bullet bullet)
        {
            var position = bullet.Prefab.transform.position;
            var viewportPosition = Camera.WorldToViewportPoint(position);

            return viewportPosition.x > 1 || viewportPosition.x < 0 ||
                    viewportPosition.y < 0 || viewportPosition.y > 1;
        }

        private void OnReceived(T poolObject)
        {
            _bullets.Add(poolObject);
        }
    }
}