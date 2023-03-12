using System;
using Constants;
using Entities.Enemy;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Containers;
using StaticData;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.Services.Factories
{
    public class EnemyFactory
    {
        private readonly AssetProvider _assetProvider;
        private readonly IUpdatable _updatable;
        private readonly EventListenerContainer _eventListenerContainer;
        private readonly Camera _camera;
        
        public event Action<IEventListener> Spawned;

        public EnemyFactory(AssetProvider assetProvider, IUpdatable updatable,
            EventListenerContainer eventListenerContainer, Camera camera)
        {
            _assetProvider = assetProvider;
            _updatable = updatable;
            _eventListenerContainer = eventListenerContainer;
            _camera = camera;
        }

        public Ufo CreateUfo(Transform playerShip)
        {
            var ufoPrefab = GetEnemyPrefab(out var data, AssetPath.Ufo);
            var ufo = new Ufo(ufoPrefab, data.Speed, playerShip, _updatable);

            InvokeAction(ufo);

            return ufo;
        }

        public Asteroid CreateAsteroid(Transform playerShip) //TODO: переделать, трудно добавлять новые корабли
        {
            var asteroidPrefab = GetEnemyPrefab(out var data, AssetPath.Asteroid);
            var asteroid = new Asteroid(asteroidPrefab, data.Speed, _updatable, _camera);
            
            InvokeAction(asteroid);

            return asteroid;
        }

        private GameObject GetEnemyPrefab(out EnemyStaticData data, string path)
        {
            data = _assetProvider.GetData<EnemyStaticData>(path);
            var asteroidPrefab = Object.Instantiate(data.Prefab);
            asteroidPrefab.gameObject.SetActive(false);

            return asteroidPrefab;
        }

        private void InvokeAction(EnemyEntityBase asteroid)
        {
            _eventListenerContainer.Register<IEventListener>(asteroid);
            Spawned?.Invoke(asteroid);
        }
    }
}