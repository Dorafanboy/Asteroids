using Infrastructure.Services.Assets;
using Infrastructure.Services.Containers;

namespace Infrastructure.Services.Factories
{
    public abstract class FactoryBase : IService
    {
        protected IAssetProvider AssetProvider { get; }
        protected EventListenerContainer EventListenerContainer { get; }
        protected TransformableContainer TransformableContainer { get; }

        protected FactoryBase(IAssetProvider assetProvider, EventListenerContainer eventListenerContainer, 
            TransformableContainer transformableContainer)
        {
            AssetProvider = assetProvider;
            EventListenerContainer = eventListenerContainer;
            TransformableContainer = transformableContainer;
        }
    }
}