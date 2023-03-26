using System;
using Entities.Enemy;
using Entities.Pool;
using Infrastructure.Services.Clashes;
using Infrastructure.Spawners.SpawnPoints;
using StaticData.Settings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Infrastructure.Spawners
{
    public class EnemySpawner : IUpdateListener 
    {
        private readonly EnemyObjectPool<EnemyEntityBase> _pool;
        private readonly Transform _playerTransform;
        private readonly Camera _camera;
        private readonly EnemySpawnerSettings _settings;
        private readonly IUpdatable _updatable;
        private readonly SpawnPointsContainer _spawnPointsContainer;
        private readonly EnemyCollisionHandler<EnemyEntityBase> _collisionHandler;

        private float _elapsedTime;

        public EnemySpawner(Transform playerTransform, EnemySpawnerSettings settings, SpawnPointsContainer spawnPoints, 
            IUpdatable updatable, Camera camera, CollisionHandler handler, params Func<Transform, EnemyEntityBase>[] createObject)
        {
            _pool = new EnemyObjectPool<EnemyEntityBase>(settings.EnemyCount, createObject);
            _collisionHandler = new EnemyCollisionHandler<EnemyEntityBase>(_pool, handler);
            _playerTransform = playerTransform;
            _settings = settings;
            _updatable = updatable;
            _camera = camera;
            _spawnPointsContainer = spawnPoints;
        }

        public void Enable()
        {
            _updatable.Updated += OnUpdated;
            _collisionHandler.AsteroidDestroyedByProjectile += OnAsteroidDestroyedByProjectile;
        }

        public void Disable()
        {
            _updatable.Updated -= OnUpdated;
            _collisionHandler.AsteroidDestroyedByProjectile -= OnAsteroidDestroyedByProjectile;
        }

        public void OnUpdated(float time)
        {
            _elapsedTime -= time;
            if (_elapsedTime <= 0)
            {
                Spawn();
                _elapsedTime = _settings.SpawnDelay;
            } 
        }

        private void OnAsteroidDestroyedByProjectile(EnemyEntityBase trans)
        {
            trans.Prefab.transform.localScale = new Vector3(1.5f, 1.5f, 0f);
            trans.Prefab.SetActive(true);
         
            var spawnedObject = _pool.GetObjectByPrefab(trans.CollisionType, _playerTransform);
            spawnedObject.Prefab.transform.position = trans.Prefab.transform.position;
            spawnedObject.Prefab.gameObject.SetActive(true);
            spawnedObject.Prefab.transform.localScale = new Vector3(1.5f, 1.5f, 0f);
        }

        private void Spawn()
        {
            var trans = _camera.ViewportToWorldPoint(GetSpawnPosition());
            trans.z = 0;
            
            var spawnedObject = _pool.GetObject(_playerTransform);
            spawnedObject.Prefab.transform.position = trans;
            spawnedObject.Prefab.transform.localScale = new Vector3(2f, 2f, 0f);
            spawnedObject.Prefab.gameObject.SetActive(true);
        }

        private Vector3 GetSpawnPosition()
        {
            var idx = Random.Range(0, _settings.SideCount);

            return _spawnPointsContainer.GetSpawnPosition(idx);
        }
    }
}