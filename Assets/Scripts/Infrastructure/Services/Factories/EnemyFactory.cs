﻿using System;
using Constants;
using Entities.Enemy;
using Infrastructure.Services.Assets;
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
        
        public EnemyFactory(IAssetProvider assetProvider, EventListenerContainer eventListenerContainer, Camera camera,
            IUpdatable updatable) : base(assetProvider, eventListenerContainer)
        {
            _updatable = updatable;
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
            data = AssetProvider.GetData<EnemyStaticData>(path);
            var asteroidPrefab = Object.Instantiate(data.Prefab);
            asteroidPrefab.gameObject.SetActive(false);

            return asteroidPrefab;
        }

        private void InvokeAction(EnemyEntityBase enemy)
        {
            EventListenerContainer.Register<IEventListener>(enemy);
        }
    }
}