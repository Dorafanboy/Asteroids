using StaticData;
using UnityEngine;

namespace Infrastructure.Services.Assets
{
    public class AssetProvider : IAssetProvider
    {
        public ShipStaticData GetShipData(string path)
        {
            var shipData = Resources.Load<ShipStaticData>(path);

            return shipData;
        }
    }
}