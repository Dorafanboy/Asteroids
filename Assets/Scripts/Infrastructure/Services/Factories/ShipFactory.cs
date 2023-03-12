using Constants;
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
    public class ShipFactory
    {
        private readonly AssetProvider _assetProvider;
        private readonly IInputService _inputService;
        private readonly EventListenerContainer _eventListenerContainer;

        public ShipFactory(AssetProvider assetProvider, IInputService inputService, EventListenerContainer eventListenerContainer)
        {
            _assetProvider = assetProvider;
            _inputService = inputService;
            _eventListenerContainer = eventListenerContainer;
        }

        public ShipModel CreateShip<T, TT>(T firstWeapon, TT secondWeapon) where T : Weapon<Bullet> where TT : Weapon<Bullet>
        {
            var shipData = _assetProvider.GetData<ShipStaticData>(AssetPath.ShipPath);
            var shipPrefab = Object.Instantiate(shipData.Prefab, Vector3.zero, Quaternion.identity);

            var ship = new ShipModel(shipData.Acceleration, shipData.Deceleration, shipData.MaxSpeed, shipData.RotationSpeed,
                shipPrefab, _inputService, firstWeapon, secondWeapon);

            var shipView = new ShipView(ship);
            var shipPresenter = new ShipPresenter(ship, shipView);
            
            _eventListenerContainer.Register<IEventListener>(shipPresenter);

            return ship;
        }
    }
}