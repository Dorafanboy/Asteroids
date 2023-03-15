using Infrastructure.Services.Containers;

namespace Infrastructure.States
{
    public class GameBehaviourState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly EventListenerContainer _updateListeners;

        public GameBehaviourState(IStateMachine stateMachine, EventListenerContainer updateListeners)
        {
            _stateMachine = stateMachine;
            _updateListeners = updateListeners;
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
            product.Enable();
        }

        private void EnableUpdateListeners()
        {
            for (int i = 0; i < _updateListeners.Listeners.Count; i++)
            {
                _updateListeners.Listeners[i].Enable();
            }
        }
        
        private void DisableUpdateListeners()
        {
            for (int i = 0; i < _updateListeners.Listeners.Count; i++)
            {
                _updateListeners.Listeners[i].Disable();
            }
        }
    }
}