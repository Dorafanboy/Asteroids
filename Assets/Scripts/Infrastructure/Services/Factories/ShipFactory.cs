using Constants;
using Entities.Guns;
using Entities.Ship;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Clashes;
using Infrastructure.Services.Containers;
using Infrastructure.Services.Inputs;
using Infrastructure.Wrapper;
using StaticData;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.Services.Factories
{
    public class ShipFactory : FactoryBase
    {
        private readonly IInputService _inputService;
        private readonly IUpdatable _updatable;
        private readonly Camera _camera;
        
        public ShipFactory(IAssetProvider assetProvider, EventListenerContainer eventListenerContainer,
            IInputService inputService, IUpdatable updatable, Camera camera, TransformableContainer transformableContainer)
            : base(assetProvider, eventListenerContainer, transformableContainer)
        {
            _inputService = inputService;
            _updatable = updatable;
            _camera = camera;
        }

        public ShipModel CreateShip<T, TT>(T firstWeapon, TT secondWeapon) where T : WeaponBase<Bullet> where TT : WeaponBase<Bullet>
        {
            var shipData = AssetProvider.GetData<ShipStaticData>(AssetPath.ShipPath);
            var shipPrefab = Object.Instantiate(shipData.Prefab, Vector3.zero, Quaternion.identity);

            var ship = new ShipModel(shipData.Acceleration, shipData.Deceleration, shipData.MaxSpeed, shipData.RotationSpeed,
                shipPrefab, _inputService, firstWeapon, secondWeapon);

            var shipView = new ShipView(ship);
            var shipPresenter = new ShipPresenter(ship, shipView);
            var wrapper = new ScreenWrapper(_updatable, _camera, shipView);
            var collision = new CollisionHandler(TransformableContainer);
            
            EventListenerContainer.Register<IEventListener>(shipPresenter);
            EventListenerContainer.Register<IEventListener>(wrapper);
            EventListenerContainer.Register<IEventListener>(collision);
            
            TransformableContainer.Register(ship.Prefab.GetComponent<CollisionChecker>());

            return ship;
        }
    }
}