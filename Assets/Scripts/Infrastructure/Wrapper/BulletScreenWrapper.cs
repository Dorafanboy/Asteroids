using System.Collections.Generic;
using System.Linq;
using Entities.Guns;
using Entities.Pool;
using UnityEngine;

namespace Infrastructure.Wrapper
{
    public class BulletScreenWrapper<T> : IUpdateListener where T : Bullet
    {
        private readonly IUpdatable _updatable;
        private readonly ObjectPool<T> _objectPool;
        private readonly Camera _camera;
        private readonly List<T> _bullets;

        public BulletScreenWrapper(IUpdatable updatable, ObjectPool<T> objectPool, Camera camera)
        {
            _updatable = updatable;
            _objectPool = objectPool;
            _camera = camera;
            _bullets = new List<T>();
        }

        public void Enable()
        {
            _updatable.Updated += OnUpdated;
            _objectPool.Received += OnReceived;
        }

        public void Disable()
        {
            _updatable.Updated -= OnUpdated;
            _objectPool.Received -= OnReceived;
        }

        public void OnUpdated(float time)
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

        private void OnReceived(T poolObject)
        {
            _bullets.Add(poolObject);
        }

        private bool IsNeedReturn(Bullet bullet)
        {
            var position = bullet.Prefab.transform.position;
            var viewportPosition = _camera.WorldToViewportPoint(position);

            return viewportPosition.x > 1 || viewportPosition.x < 0 ||
                    viewportPosition.y < 0 || viewportPosition.y > 1;
        }
    }
}