using UnityEngine;

namespace Entities.Guns
{
    public interface ITransformable
    {
        GameObject Prefab { get; }
        void InstallPosition(Vector3 position);
    }
}