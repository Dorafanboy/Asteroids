using System.Collections.Generic;
using System.Linq;
using Entities.Guns;
using UnityEngine;

namespace Infrastructure.Wrapper
{
    public class BulletScreenWrapper<T> : IUpdateListener where T : Bullet
    {
        private readonly IUpdatable _updatable;
        private readonly List<T> _projectiles;
        private readonly ObjectPool<T> _objectPool;
        private readonly Camera _camera;
        private readonly IWeapon<T> _weapon;

        public BulletScreenWrapper(IUpdatable updatable, ObjectPool<T> objectPool, IWeapon<T> weapon)
        {
            _updatable = updatable;
            _projectiles = new List<T>();
            _objectPool = objectPool;
            _weapon = weapon;
            _camera = Camera.main;

            Enable();
        }

        public void Enable()
        {
            _updatable.Updated += OnUpdated;
            _weapon.Fired += OnFired;
        }

        public void Disable()
        {
            _updatable.Updated -= OnUpdated;
            _weapon.Fired -= OnFired;
        }

        public void OnUpdated(float time)
        {
            if (_projectiles == null)
            {
                return;
            }

            foreach (var proj in _projectiles.ToList())
            {
                if (IsNeedReturn(proj))
                {
                    proj.Prefab.SetActive(false);

                    _objectPool.ReturnObject(proj);

                    _projectiles.Remove(proj);
                }
            }
        }

        private bool IsNeedReturn(Bullet bullet)
        {
            var position = bullet.Prefab.transform.position;
            var viewportPosition = _camera.WorldToViewportPoint(position);

            return (viewportPosition.x > 1 || viewportPosition.x < 0 ||
                    viewportPosition.y < 0 || viewportPosition.y > 1);
        }

        private void OnFired(Bullet bullet)
        {
            _projectiles.Add((T)bullet);
        }
    }
}