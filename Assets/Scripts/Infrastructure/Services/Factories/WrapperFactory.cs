using Entities.Ship;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Containers;
using Infrastructure.Wrapper;
using UnityEngine;

namespace Infrastructure.Services.Factories
{
    public class WrapperFactory : FactoryBase
    {
        private readonly IUpdatable _updatable;
        private readonly Camera _camera;
        
        public WrapperFactory(IAssetProvider assetProvider, EventListenerContainer eventListenerContainer,
            IUpdatable updatable, Camera camera) : base(assetProvider, eventListenerContainer)
        {
            _updatable = updatable;
            _camera = camera;
        }
        
        public ScreenWrapper CreateWrapper(ShipModel shipModel)
        {
            var wrapper = new ScreenWrapper(_updatable, shipModel, _camera);
            EventListenerContainer.Register<IEventListener>(wrapper);
            
            return wrapper;
        }
    }
}