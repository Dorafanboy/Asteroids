using System.Collections.Generic;
using Infrastructure.Services.Containers;
using IFactory = Infrastructure.Services.Factories.IFactory;

namespace Infrastructure.States
{
    public class GameBehaviourState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly EventListenerContainer _updateListeners;
        private readonly IFactory _factory;
        private readonly List<IEventListener> _listeners;

        public GameBehaviourState(IStateMachine stateMachine, EventListenerContainer updateListeners, IFactory factory)
        {
            _stateMachine = stateMachine;
            _updateListeners = updateListeners;
            _factory = factory;
            _listeners = _updateListeners.Lesten; 
            //TODO: сделать потом переход из этого стейта, при рестарте там, спмерти и тд
        }
        
        public void Enter()
        {
            _factory.Spawned += OnSpawned;
            
            EnableUpdateListeners();
        }

        public void Exit()
        {
            _factory.Spawned -= OnSpawned;

            DisableUpdateListeners();
        }

        private void OnSpawned(IEventListener product)
        {
            _listeners.Add(product);
            product.Enable();
        }

        private void EnableUpdateListeners()
        {
            foreach (var listener in _updateListeners.Lesten)
            {
                listener.Enable();
            }
        }
        
        private void DisableUpdateListeners()
        {
            foreach (var listener in _updateListeners.Lesten)
            {
                listener.Disable();
            }
        }
    }
}