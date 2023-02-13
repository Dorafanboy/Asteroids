using System.Collections.Generic;
using Guns;
using UnityEngine;

namespace Infrastructure.Wrapper
{
    public class BulletScreenWrapper : IUpdateListener
    {
        private readonly IUpdatable _updatable;
        private readonly Queue<Projectile> _projectiles;
        private readonly ObjectPool<Projectile> _objectPool;
        private readonly Camera _camera;
        private readonly Weapon _weapon;

        public BulletScreenWrapper(IUpdatable updatable, ObjectPool<Projectile> objectPool, Weapon weapon)
        {
            _updatable = updatable;
            _projectiles = new Queue<Projectile>();
            _objectPool = objectPool;
            _weapon = weapon;
            _camera = Camera.main;

            Enable();
        }

        public void Enable()
        {
            _updatable.Updated += OnUpdated;
            _weapon.Shooted += OnShooted;
        }

        public void Disable()
        {
            _updatable.Updated -= OnUpdated;
            _weapon.Shooted -= OnShooted;
        }

        public void OnUpdated(float time)
        {
            if (_projectiles == null)
            {
                return;
            }
            
            foreach (var proj in _projectiles)
            {
                GetWrapPosition(proj);
            }
        }

        private void GetWrapPosition(Projectile projectile)
        {
            var position = projectile.Prefab.transform.position;
            var viewportPosition = _camera.WorldToViewportPoint(position);
            
            if (viewportPosition.x > 1 || viewportPosition.x < 0 || 
                viewportPosition.y < 0 || viewportPosition.y > 1)
            {
                projectile.Prefab.SetActive(false);
                
                _objectPool.ReturnObject(projectile);
            }
        }

        private void OnShooted(Projectile projectile)
        {
            _projectiles.Enqueue(projectile);
        }
    }
}