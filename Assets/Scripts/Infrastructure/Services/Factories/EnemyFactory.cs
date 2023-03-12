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
        
        public event Action<IEventListener> Spawned;

        public EnemyFactory(AssetProvider assetProvider, IUpdatable updatable, EventListenerContainer eventListenerContainer)
        {
            _assetProvider = assetProvider;
            _updatable = updatable;
            _eventListenerContainer = eventListenerContainer;
        }

        public Ufo CreateUfo(Transform playerShip)
        {
            var ufoData = _assetProvider.GetData<EnemyStaticData>(AssetPath.Ufo);
            var asteroidPrefab = Object.Instantiate(ufoData.Prefab);
            var ufo = new Ufo(asteroidPrefab, ufoData.Speed, playerShip, _updatable);
            asteroidPrefab.gameObject.SetActive(false);
            
            _eventListenerContainer.Register<IEventListener>(ufo);
            Spawned?.Invoke(ufo);

            return ufo;
        }

        public Asteroid CreateAsteroid(Transform playerShip)
        {
            var asteroidData = _assetProvider.GetData<EnemyStaticData>(AssetPath.Asteroid);
            var asteroidPrefab = Object.Instantiate(asteroidData.Prefab, Vector3.zero, Quaternion.identity);
            var asteroid = new Asteroid(asteroidPrefab, asteroidData.Speed, _updatable, Camera.main);
            asteroidPrefab.gameObject.SetActive(false);
            
            _eventListenerContainer.Register<IEventListener>(asteroid);
            
            Spawned?.Invoke(asteroid);

            return asteroid;
        }
    }
}