using System;
using System.Collections.Generic;
using Entities.Guns;
using Infrastructure;
using Infrastructure.Services.Factories;
using StaticData.Settings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities.Enemy
{
    public class EnemySpawner : IUpdateListener
    {
        private readonly AsteroidObjectPool<EnemyEntityBase> _pool;
        private readonly Transform _playerTransform;
        private readonly IFactory _factory;
        private readonly Camera _camera;
        private readonly EnemySpawnerSettings _settings;
        private readonly IUpdatable _updatable;
        private SpawnPointsContainer _spawnPointsContainer;

        private float _elapsedTime;

        public event Action<EnemyEntityBase> Spawned;

        public EnemySpawner(AsteroidObjectPool<EnemyEntityBase> pool, Transform playerTransform, IFactory factory,
            EnemySpawnerSettings settings, SpawnPointsContainer spawnPoints, IUpdatable updatable)
        {
            _pool = pool;
            _playerTransform = playerTransform;
            _factory = factory;
            _settings = settings;
            _updatable = updatable;
            _camera = Camera.main;
            _spawnPointsContainer = spawnPoints;

            Enable();
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
            _elapsedTime -= Time.deltaTime;
            if (_elapsedTime <= 0)
            {
                Debug.Log("Spawn");
                Spawn();
                _elapsedTime = _settings.SpawnDelay;
            }
        }

        private void Spawn()
        {
            // var trans = _camera.ViewportToWorldPoint(GetSpawnPosition());
            //
            // trans.z = 0;
            //
            // var spawnedObject = _factory.CreateAsteroid(_playerTransform);
            //
            // spawnedObject.Prefab.transform.position = trans;
            // _pool.ReturnObject(spawnedObject);
            //
            // Spawned?.Invoke(spawnedObject);
            //
            // trans = _camera.ViewportToWorldPoint(GetSpawnPosition());
            // var spawnedObjectUfo = _factory.CreateUfo(_playerTransform);
            //
            // spawnedObjectUfo.Prefab.transform.position = trans;
            // _pool.ReturnObject(spawnedObjectUfo);
            //
            // Spawned?.Invoke(spawnedObjectUfo);
            
            var trans = _camera.ViewportToWorldPoint(GetSpawnPosition());

            trans.z = 0;
            
            var spawnedObject = _pool.GetObject(_playerTransform);

            spawnedObject.Prefab.transform.position = trans;
            spawnedObject.Prefab.gameObject.SetActive(true);

            Spawned?.Invoke(spawnedObject);
        }

        private Vector3 GetSpawnPosition()
        {
            var idx = Random.Range(0, _settings.SideCount);

            return _spawnPointsContainer.GetSpawnPosition(idx);
        }
    }
}

public class BulletSpawner
{
    private readonly ObjectPool<Bullet> _pool;
    private readonly IFactory _factory;

    public BulletSpawner(ObjectPool<Bullet> pool, IFactory factory)
    {
        _pool = pool;
        _factory = factory;
    }

    public void Spawn()
    {
    }
}

public abstract class EnemyEntityBase : IPoolProduct
{
    public GameObject Prefab { get; }
    protected float Speed { get; }

    protected EnemyEntityBase(GameObject prefab, float speed)
    {
        Prefab = prefab;
        Speed = speed;
    }

    //TODO Сделать обработку коллизий
}

public class Ufo : EnemyEntityBase, IUpdateListener
{
    private readonly Transform _playerShip;
    private readonly IUpdatable _updatable;

    public Ufo(GameObject prefab, float speed, Transform playerShip, IUpdatable updatable) : base(prefab, speed)
    {
        _playerShip = playerShip;
        _updatable = updatable;

        Enable();
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
        Prefab.gameObject.transform.position = Vector3.Lerp(Prefab.gameObject.transform.position,
            _playerShip.transform.position, time * Speed);
    }
}

public class Asteroid : EnemyEntityBase, IUpdateListener
{
    private readonly IUpdatable _updatable;
    private readonly Transform _playerShip;
    private float timeToMove = 0f;
    private Vector3 targetPosition;
    public float radius = 0.5f;

    public Asteroid(GameObject prefab, float speed, IUpdatable updatable, Transform playerShip) : base(prefab, speed)
    {
        _updatable = updatable;
        _playerShip = playerShip;

        Vector2 randomPoint = Random.insideUnitCircle * radius;
        targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width),
            Random.Range(0, Screen.height), 0));

        Enable();
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
        Prefab.gameObject.transform.position = Vector3.MoveTowards(Prefab.gameObject.transform.position,
            targetPosition, Speed * Time.deltaTime);
        
        if (Prefab.gameObject.transform.position == targetPosition)
        {
            targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width),
                Random.Range(0, Screen.height), 0));

            targetPosition.z = 0;
        }
    }
}

public interface ISpawnBehaviour
{
    float Position { get; }
    Vector3 GetSpawnPosition();
}

public readonly struct TopSpawnBehaviour : ISpawnBehaviour
{
    public float Position { get; }

    public TopSpawnBehaviour(float position)
    {
        Position = position;
    }

    public Vector3 GetSpawnPosition()
    {
        return new Vector3(Random.Range(0f, 1f), Position, 0);
    }
}

public readonly struct BottomSpawnBehaviour : ISpawnBehaviour
{
    public float Position { get; }

    public BottomSpawnBehaviour(float position)
    {
        Position = position;
    }

    public Vector3 GetSpawnPosition()
    {
        return new Vector3(Random.Range(0f, 1f), Position, 0);
    }
}

public readonly struct LeftSpawnBehaviour : ISpawnBehaviour
{
    public float Position { get; }

    public LeftSpawnBehaviour(float position)
    {
        Position = position;
    }

    public Vector3 GetSpawnPosition()
    {
        return new Vector3(Position, Random.Range(0f, 1f), 0);
    }
}

public readonly struct RightSpawnBehaviour : ISpawnBehaviour
{
    public float Position { get; }

    public RightSpawnBehaviour(float position)
    {
        Position = position;
    }

    public Vector3 GetSpawnPosition()
    {
        return new Vector3(Position, Random.Range(0f, 1f), 0);
    }
}

public readonly struct SpawnPointsContainer
{
    private readonly List<ISpawnBehaviour> _spawns;

    public SpawnPointsContainer(List<ISpawnBehaviour> vectors)
    {
        _spawns = vectors;
    }

    public Vector3 GetSpawnPosition(int idx)
    {
        return _spawns[idx].GetSpawnPosition();
    }
}