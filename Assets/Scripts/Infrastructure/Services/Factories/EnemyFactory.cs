using Constants;
using Entities.Enemy;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Clashes;
using Infrastructure.Services.Containers;
using StaticData;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.Services.Factories
{
    public class EnemyFactory : FactoryBase
    {
        private readonly IUpdatable _updatable;
        private readonly Camera _camera;
        private readonly int _asteroidCount;

        public EnemyFactory(IAssetProvider assetProvider, EventListenerContainer eventListenerContainer, Camera camera,
            IUpdatable updatable, TransformableContainer transformableContainer)
            : base(assetProvider, eventListenerContainer, transformableContainer)
        {
            _updatable = updatable;
            _camera = camera;
            _asteroidCount = 2;
        }

        public Ufo CreateUfo(Transform playerShip)
        {
            var ufoPrefab = GetEnemyPrefab(out var data, AssetPath.Ufo);
            var ufo = new Ufo(ufoPrefab, data.Speed, _updatable, CollisionType.Ufo, playerShip);

            InvokeAction(ufo);

            return ufo;
        }

        public Asteroid CreateAsteroid(Transform playerShip) //TODO: переделать, трудно добавлять новые корабли
        {
            var asteroidPrefab = GetEnemyPrefab(out var data, AssetPath.Asteroid);
            var asteroid = new Asteroid(asteroidPrefab, data.Speed, _updatable, CollisionType.Asteroid, _camera, false);
            
            for (int i = 0; i < _asteroidCount; i++)
            {
                var miniAsteroidPrefab = GetEnemyPrefab(out data, AssetPath.Asteroid);
                miniAsteroidPrefab.transform.localScale /= 2;
                
                var miniAsteroid = new Asteroid(miniAsteroidPrefab, data.Speed / 2, _updatable, CollisionType.Asteroid, 
                    _camera, true);
                asteroid.Add(miniAsteroid);
                TransformableContainer.RegisterObject(miniAsteroid.Prefab.GetComponent<CollisionChecker>());
                EventListenerContainer.Register<IEventListener>(miniAsteroid);
            }

            InvokeAction(asteroid);

            return asteroid;
        }

        private GameObject GetEnemyPrefab(out EnemyStaticData data, string path)
        {
            data = AssetProvider.GetData<EnemyStaticData>(path);
            var asteroidPrefab = Object.Instantiate(data.Prefab);
            asteroidPrefab.gameObject.SetActive(false);

            return asteroidPrefab;
        }

        private void InvokeAction(EnemyEntityBase enemy)
        {
            EventListenerContainer.Register<IEventListener>(enemy);
            TransformableContainer.RegisterObject(enemy.Prefab.GetComponent<CollisionChecker>());
        }
    }
}