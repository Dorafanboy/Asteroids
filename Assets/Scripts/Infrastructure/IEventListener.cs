using Infrastructure.Services;

namespace Infrastructure
{
    public interface IEventListener : IService
    {
        void Enable();
        void Disable();
    }
}