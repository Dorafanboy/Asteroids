using System;

namespace Infrastructure.Services.Inputs
{
    public interface IInputService : IService
    {
        event Action<float> RotateKeyDowned;
        event Action<bool, float> MoveKeyDowned;
        event Action<float> MoveKeyUpped;
    }
}