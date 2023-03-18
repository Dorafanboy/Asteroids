using Infrastructure.Services.Containers;

namespace Infrastructure.States
{
    public class GameBehaviourState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly EventListenerContainer _eventListener;

        public GameBehaviourState(IStateMachine stateMachine, EventListenerContainer eventListener,
            TransformableContainer transformableContainer)
        {
            _stateMachine = stateMachine;
            _eventListener = eventListener;
            //TODO: сделать потом переход из этого стейта, при рестарте там, спмерти и тд
        }
        
        public void Enter()
        {
            _eventListener.Registered += OnRegistered;
            EnableUpdateListeners();
        }

        public void Exit()
        {
            _eventListener.Registered -= OnRegistered;
            DisableUpdateListeners();
        }

        private void OnRegistered(IEventListener product)
        {
            product.Enable();
        }

        private void EnableUpdateListeners()
        {
            for (int i = 0; i < _eventListener.Listeners.Count; i++)
            {
                _eventListener.Listeners[i].Enable();
            }
        }
        
        private void DisableUpdateListeners()
        {
            for (int i = 0; i < _eventListener.Listeners.Count; i++)
            {
                _eventListener.Listeners[i].Disable();
            }
        }
    }
}