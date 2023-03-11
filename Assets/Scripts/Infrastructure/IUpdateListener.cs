using Infrastructure.Services;

namespace Infrastructure
{
    public interface IUpdateListener : IEventListener
    {
        void OnUpdated(float time);
    }
}