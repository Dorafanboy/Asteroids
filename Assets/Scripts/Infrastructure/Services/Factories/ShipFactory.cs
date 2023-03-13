﻿using Constants;
using Entities.Guns;
using Entities.Ship;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Containers;
using Infrastructure.Services.Inputs;
using StaticData;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.Services.Factories
{
    public class ShipFactory : FactoryBase
    {
        private readonly IInputService _inputService;
        
        public ShipFactory(IAssetProvider assetProvider, EventListenerContainer eventListenerContainer,
            IInputService inputService) : base(assetProvider, eventListenerContainer)
        {
            _inputService = inputService;
        }

        public ShipModel CreateShip<T, TT>(T firstWeapon, TT secondWeapon) where T : WeaponBase<Bullet> where TT : WeaponBase<Bullet>
        {
            var shipData = AssetProvider.GetData<ShipStaticData>(AssetPath.ShipPath);
            var shipPrefab = Object.Instantiate(shipData.Prefab, Vector3.zero, Quaternion.identity);

            var ship = new ShipModel(shipData.Acceleration, shipData.Deceleration, shipData.MaxSpeed, shipData.RotationSpeed,
                shipPrefab, _inputService, firstWeapon, secondWeapon);

            var shipView = new ShipView(ship);
            var shipPresenter = new ShipPresenter(ship, shipView);
            
            EventListenerContainer.Register<IEventListener>(shipPresenter);

            return ship;
        }
    }
}