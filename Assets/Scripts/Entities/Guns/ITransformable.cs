using System;
using Infrastructure.Services;
using UnityEngine;

namespace Entities.Guns
{
    public interface ITransformable : IService
    {
        GameObject Prefab { get; }
        void InstallPosition(Vector3 position);
        void DisableObject();
        event Action<ITransformable> Collided;
    }
}