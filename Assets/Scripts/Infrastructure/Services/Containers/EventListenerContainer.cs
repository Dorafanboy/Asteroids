using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Services.Containers
{
    public class EventListenerContainer
    {
        private readonly List<IEventListener> _listeners;

        public event Action<IEventListener> Registered;

        public List<IEventListener> Lesten => _listeners;

        public EventListenerContainer()
        {
            _listeners = new List<IEventListener>();
        }

        public void Register<TService>(TService service) where TService : IService
        {
            _listeners.Add((IEventListener)service);
            
            Registered?.Invoke((IEventListener)service);
        }
        
        public IEnumerable<IEventListener> GetServices()
        {
            var services = new List<IEventListener>();
            foreach (var item in _listeners)
            {
                Debug.Log("Get service" + item);

                Debug.Log(item);
            }

            return _listeners;
        }
    }
}