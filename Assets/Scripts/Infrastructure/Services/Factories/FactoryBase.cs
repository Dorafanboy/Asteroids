using Infrastructure.Services.Assets;
using Infrastructure.Services.Containers;

namespace Infrastructure.Services.Factories
{
    public abstract class FactoryBase : IService
    {
        protected IAssetProvider AssetProvider { get; }
        protected EventListenerContainer EventListenerContainer { get; }

        protected FactoryBase(IAssetProvider assetProvider, EventListenerContainer eventListenerContainer)
        {
            AssetProvider = assetProvider;
            EventListenerContainer = eventListenerContainer;
        }
    }
}