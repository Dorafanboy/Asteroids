using System;
using System.Collections.Generic;
using Entities.Guns;
using Infrastructure.Services;
using Infrastructure.Services.Clashes;

namespace Infrastructure.Services.Containers
{
    public class EventListenerContainer : IContainer<IEventListener>
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

public class TransformableContainer : IContainer<CollisionChecker>
{
    private readonly List<CollisionChecker> _listeners;
    public IReadOnlyList<CollisionChecker> Listeners => _listeners;
    public event Action<CollisionChecker> Registered;

    public TransformableContainer()
    {
        _listeners = new List<CollisionChecker>();
    }

    public void Register<TService>(TService service) where TService : IService
    {
    }
    
    public void RegisterObject(CollisionChecker checker)
    {
        _listeners.Add(checker);
        
        Registered?.Invoke(checker);
    }
}

public interface IContainer<T> where T : IService
{
    void Register<TService>(TService service) where TService : IService;
}