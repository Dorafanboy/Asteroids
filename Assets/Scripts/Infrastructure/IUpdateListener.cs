using Infrastructure.Services;

namespace Infrastructure
{
    public interface IUpdateListener
    {
        void Enable();
        void Disable();
        void OnUpdated(float time);
    }
}