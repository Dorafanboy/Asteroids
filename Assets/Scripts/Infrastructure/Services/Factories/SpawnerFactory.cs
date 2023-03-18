using System.Collections.Generic;
using Constants;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Containers;
using Infrastructure.Spawners;
using Infrastructure.Spawners.SpawnPoints;
using StaticData.Settings;
using UnityEngine;

namespace Infrastructure.Services.Factories
{
    public class SpawnerFactory : FactoryBase
    {
        private readonly IUpdatable _updatable;
        private readonly EnemyFactory _enemyFactory;
        private readonly Camera _camera;

        public SpawnerFactory(IAssetProvider assetProvider, EventListenerContainer eventListenerContainer,
            IUpdatable updatable, EnemyFactory enemyFactory, Camera camera, TransformableContainer transformableContainer) 
            : base(assetProvider, eventListenerContainer, transformableContainer)
        {
            _updatable = updatable;
            _enemyFactory = enemyFactory;
            _camera = camera;
        }
        
        public EnemySpawner CreateEnemySpawner(Transform prefabTransform)   //TODO сделать сущность которая за спавны будет отвечать мб
        {
            var settings = AssetProvider.GetData<EnemySpawnerSettings>(AssetPath.EnemySpawnerSettings);
            var enemySpawner = new EnemySpawner(prefabTransform, settings, CreateSpawnPointsContainer(),
                _updatable, _camera, _enemyFactory.CreateAsteroid, _enemyFactory.CreateUfo);

            EventListenerContainer.Register<IEventListener>(enemySpawner);
        
            return enemySpawner;
        }

        private SpawnPointsContainer CreateSpawnPointsContainer()
        {
            var list = new List<ISpawnBehaviour>()
            {
                new TopSpawnBehaviour(SpawnPoints.TopEdge),
                new BottomSpawnBehaviour(SpawnPoints.BottomEdge),
                new RightSpawnBehaviour(SpawnPoints.LeftEdge),
                new LeftSpawnBehaviour(SpawnPoints.RightEdge),
            };

            return new SpawnPointsContainer(list);
        }

        // private BulletSpawner CreateBulletSpawner()

        // {

        //     return new BulletSpawner();

        // }
    }
}