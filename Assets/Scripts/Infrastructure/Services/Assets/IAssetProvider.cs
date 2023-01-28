using StaticData;
using UnityEngine;

namespace Infrastructure.Services.Assets
{
    public interface IAssetProvider : IService
    {
        ShipStaticData GetShipData(string path, Vector3 position);
    }
}