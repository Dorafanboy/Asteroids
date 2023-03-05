using System;
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
        private readonly ObjectPool<EnemyEntityBase> _pool;
        private readonly Transform _playerTransform;
        private readonly IFactory _factory;
        private readonly Camera _camera;
        private readonly EnemySpawnerSettings _settings;
        private readonly IUpdatable _updatable;
        
        public event Action<EnemyEntityBase> Spawned;

        public EnemySpawner(ObjectPool<EnemyEntityBase> pool, Transform playerTransform, IFactory factory, EnemySpawnerSettings settings)
        {
            _pool = pool;
            _playerTransform = playerTransform;
            _factory = factory;
            _settings = settings;
            // _updatable = updatable;  EnemySpawnerSettings settings, IUpdatable updatable
            _camera = Camera.main;
            
           // Enable();
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
            
        }

        public void Spawn(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var r = Random.Range(0, _settings.CameraBounds.Count);
                var random = _settings.CameraBounds[r].SpawnPositions;
                var trans = _camera.ViewportToWorldPoint(random);
                trans.z = 0;
                
                if (i % 2 == 0)
                {
                    var spawnedObject = _factory.CreateAsteroid();
                    
                    spawnedObject.Prefab.transform.position = trans;
                    _pool.ReturnObject(spawnedObject);
                    
                    Spawned?.Invoke(spawnedObject);
                }
                else
                {
                    var spawnedObject = _factory.CreateUfo(_playerTransform);
                    
                    spawnedObject.Prefab.transform.position = trans;
                    _pool.ReturnObject(spawnedObject);
                    
                    Spawned?.Invoke(spawnedObject);
                }
            }
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

    public Asteroid(GameObject prefab, float speed, IUpdatable updatable) : base(prefab, speed)
    {
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
        Prefab.gameObject.transform.position += Vector3.right * time * Speed; 
    }
}