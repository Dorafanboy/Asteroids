using System;
using Entities.Enemy;
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

        private float _elapsedTime;

        public EnemySpawner(Transform playerTransform, EnemySpawnerSettings settings, 
            SpawnPointsContainer spawnPoints, IUpdatable updatable, Camera camera, params Func<Transform, EnemyEntityBase>[] createObject)
        {
            _pool = new EnemyObjectPool<EnemyEntityBase>(settings.EnemyCount, createObject);
            _playerTransform = playerTransform;
            _settings = settings;
            _updatable = updatable;
            _camera = camera;
            _spawnPointsContainer = spawnPoints;
        }

        public void Enable()
        {
            _updatable.Updated += OnUpdated;
        }

        public void Disable()
        {
            _updatable.Updated -= OnUpdated;
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

        private void Spawn()
        {
            var trans = _camera.ViewportToWorldPoint(GetSpawnPosition());
            trans.z = 0;
            
            var spawnedObject = _pool.GetObject(_playerTransform);
            spawnedObject.Prefab.transform.position = trans;
            spawnedObject.Prefab.gameObject.SetActive(true);
        }

        private Vector3 GetSpawnPosition()
        {
            var idx = Random.Range(0, _settings.SideCount);

            return _spawnPointsContainer.GetSpawnPosition(idx);
        }
    }
}