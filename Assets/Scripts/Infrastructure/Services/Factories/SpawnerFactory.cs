using System;
using System.Collections.Generic;
using Constants;
using Entities.Enemy;
using Entities.Pool;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Containers;
using Infrastructure.Spawners;
using Infrastructure.Spawners.SpawnPoints;
using Infrastructure.Wrapper;
using StaticData.Settings;
using UnityEngine;

namespace Infrastructure.Services.Factories
{
    public class SpawnerFactory
    {
        private readonly AssetProvider _assetProvider;
        private readonly IUpdatable _updatable;
        private readonly EventListenerContainer _eventListenerContainer;
        private readonly EnemyFactory _enemyFactory;

        public SpawnerFactory(AssetProvider assetProvider, IUpdatable updatable, EventListenerContainer eventListenerContainer)
        {
            _assetProvider = assetProvider;
            _updatable = updatable;
            _eventListenerContainer = eventListenerContainer;
        }

        public EnemySpawner CreateEnemySpawner(Transform prefabTransform)
        {
            var settings = _assetProvider.GetData<EnemySpawnerSettings>(AssetPath.EnemySpawnerSettings);
            var enemySpawner = new EnemySpawner(prefabTransform, settings, CreateSpawnPointsContainer(),
                _updatable, _enemyFactory.CreateAsteroid, _enemyFactory.CreateUfo);
            
            //TODO сделать сущность которая за спавны будет отвечать мб
            
            _eventListenerContainer.Register<IEventListener>(enemySpawner);
        
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