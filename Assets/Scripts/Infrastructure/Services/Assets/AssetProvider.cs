using UnityEngine;

namespace Infrastructure.Services.Assets
{
    public class AssetProvider : IAssetProvider
    {
        public T GetData<T>(string path) where T : ScriptableObject
        {
            var data = Resources.Load<T>(path);

            return data;
        }
    }
}