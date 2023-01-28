using System;

namespace Infrastructure
{
    public interface IUpdatable
    {
        event Action<float> Updated;
    }
}