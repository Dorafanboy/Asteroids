using System.Collections.Generic;
using Infrastructure.Services.Containers;

namespace Infrastructure.States
{
    public class GameBehaviourState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly EventListenerContainer _updateListeners;
        private readonly List<IEventListener> _listeners;

        public GameBehaviourState(IStateMachine stateMachine, EventListenerContainer updateListeners)
        {
            _stateMachine = stateMachine;
            _updateListeners = updateListeners;
            _listeners = _updateListeners.Lesten; 
            //TODO: сделать потом переход из этого стейта, при рестарте там, спмерти и тд
        }
        
        public void Enter()
        {
            _updateListeners.Registered += OnRegistered;
            
            EnableUpdateListeners();
        }

        public void Exit()
        {
            _updateListeners.Registered -= OnRegistered;

            DisableUpdateListeners();
        }

        private void OnRegistered(IEventListener product)
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