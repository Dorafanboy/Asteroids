using System;
using System.Collections.Generic;

namespace Infrastructure.Services.Containers
{
    public class EventListenerContainer
    {
        private readonly List<IEventListener> _listeners;
        public IReadOnlyList<IEventListener> Listeners => _listeners;
        public event Action<IEventListener> Registered;

        public EventListenerContainer()
        {
            _listeners = new List<IEventListener>();
        }

        public void Register<TService>(TService service) where TService : IService
        {
            _listeners.Add((IEventListener)service);
            
            Registered?.Invoke((IEventListener)service);
        }
    }
}