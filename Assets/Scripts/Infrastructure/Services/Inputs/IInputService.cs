using System;

namespace Infrastructure.Services.Inputs
{
    public interface IInputService : IService
    {
        event Action<float> KeyDowned;
    }
}