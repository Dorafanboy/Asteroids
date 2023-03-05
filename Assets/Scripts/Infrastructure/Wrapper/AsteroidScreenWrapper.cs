using System.Collections.Generic;
using System.Linq;
using Entities.Enemy;
using Entities.Guns;
using UnityEngine;

namespace Infrastructure.Wrapper
{
    public class AsteroidScreenWrapper<T> : IUpdateListener where T : EnemyEntityBase
    {
        private readonly IUpdatable _updatable;
        private readonly List<EnemyEntityBase> _asteroids;
        private readonly ObjectPool<T> _objectPool;
        private readonly Camera _camera;
        private readonly EnemySpawner _spawner;

        public AsteroidScreenWrapper(IUpdatable updatable, ObjectPool<T> objectPool, EnemySpawner spawner)
        {
            _updatable = updatable;
            _asteroids = new List<EnemyEntityBase>();
            _objectPool = objectPool;
            _camera = Camera.main;
            _spawner = spawner;// через констуктор передать

            Enable();
        }

        public void Enable()
        {
            _updatable.Updated += OnUpdated;
            _spawner.Spawned += OnSpawned;
        }

        public void Disable()
        {
            _updatable.Updated -= OnUpdated;
            _spawner.Spawned -= OnSpawned;
        }

        public void OnUpdated(float time)
        {
            if (_asteroids == null)
            {
                Debug.Log("no Have elemtn in asteroid wrap");

                return;
            }

            foreach (var proj in _asteroids.ToList()) // через for
            {
                Debug.Log("Have elemtn in asteroid wrap " + _asteroids.Count);
                var position = proj.Prefab.transform.position;
                var viewportPosition = _camera.WorldToViewportPoint(position);
                var newPosition = position;

                newPosition.x = GetWrapPosition(viewportPosition.x, newPosition.x);
                newPosition.y = GetWrapPosition(viewportPosition.y, newPosition.y);
        
                proj.Prefab.transform.position = newPosition;
            }
        }

        private void OnSpawned(EnemyEntityBase obj)
        {
            _asteroids.Add(obj);
        }

        private float GetWrapPosition(float viewportPosition, float newPosition)
        {
            return viewportPosition > 1 || viewportPosition < 0 ? -newPosition : newPosition;
        }
    }
}