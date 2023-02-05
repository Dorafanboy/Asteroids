using UnityEngine;

namespace Infrastructure.Services.Assets
{
    public interface IAssetProvider : IService
    {
        T GetData<T>(string path) where T : ScriptableObject;
    }
}