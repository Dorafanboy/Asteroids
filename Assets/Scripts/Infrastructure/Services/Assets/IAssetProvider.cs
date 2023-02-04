using StaticData;

namespace Infrastructure.Services.Assets
{
    public interface IAssetProvider : IService
    {
        ShipStaticData GetShipData(string path);
    }
}