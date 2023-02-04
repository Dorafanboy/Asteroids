using System;
using UnityEngine;

namespace Infrastructure.Services.Inputs
{
    public interface IInputService : IService
    {
        event Action<Vector2, float> MoveKeyDowned;
        event Action<float, float> RotateKeyDowned;
    }
}