using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Services.Containers
{
    public class EventListenerContainer
    {
        private readonly List<IEventListener> _listeners;

        public List<IEventListener> Lesten => _listeners;

        public EventListenerContainer()
        {
            _listeners = new List<IEventListener>();
        }

        public void Register<TService>(TService service) where TService : IService
        {
            Debug.Log("ADD");
            _listeners.Add((IEventListener)service);
        }
        
        public IEnumerable<IEventListener> GetServices()
        {
            var services = new List<IEventListener>();
            Debug.Log("Get service" + _listeners.Count);
            foreach (var item in _listeners)
            {
                Debug.Log("Get service" + item);

                Debug.Log(item);
            }

            return _listeners;
        }
    }
}