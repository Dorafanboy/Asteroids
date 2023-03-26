using System;
using Infrastructure.Services;
using Infrastructure.Services.Clashes;
using UnityEngine;

namespace Entities.Guns
{
    public interface ITransformable : IService, ICollideable
    {
        GameObject Prefab { get; }
        void InstallPosition(Vector3 position);
        void DisableObject();
        event Action<ITransformable> Collided;
    }
}

public interface ICollideable
{
    CollisionType CollisionType { get; }
}