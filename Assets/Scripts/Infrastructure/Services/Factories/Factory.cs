using UnityEngine;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Inputs;
using Infrastructure.Wrapper;
using ShipContent;

namespace Infrastructure.Services.Factories
{
    public class Factory : IFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IUpdatable _updatable;
        private readonly IInputService _inputService;

        public Factory(IAssetProvider assetProvider, IUpdatable updatable, IInputService inputService)
        {
            _assetProvider = assetProvider;
            _updatable = updatable;
            _inputService = inputService;
        }

        public Ship CreateShip()
        {
            var shipData = _assetProvider.GetShipData(AssetPath.ShipPath, Vector3.zero);
            var shipPrefab = Object.Instantiate(shipData.Prefab, Vector3.zero, Quaternion.identity);
            
            var ship = new Ship(shipData.Acceleration, shipData.Deceleration, shipData.MaxSpeed, 
                shipData.RotationSpeed, shipData.ShotCooldown, shipData.MaxAmmo, shipPrefab);

            var shipView = new ShipView(ship, _inputService);
            var shipPresenter = new ShipPresenter(ship, shipView);
            
            
            
            return ship;
        }

        public ScreenWrapper CreateWrapper(Ship ship)
        {
            var wrapper = new ScreenWrapper(_updatable, ship);
            wrapper.Enable();

            return wrapper;
        }
    }
}