using System;
using System.Collections.Generic;
using Infrastructure.Loaders;
using Infrastructure.Services;
using Infrastructure.Services.Factories;

namespace Infrastructure.States
{
    public class StateMachine : IStateMachine
    {
        private readonly DiContainer _diContainer;
        
        private readonly Dictionary<Type, IState> _states;
        private IState _currentState;

        public StateMachine(ISceneLoader sceneLoader, ICoroutineRunner coroutineRunner, IUpdatable updatable)
        {
            _diContainer = new DiContainer();
            
            _states = new Dictionary<Type, IState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, _diContainer, sceneLoader, updatable),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader),
                [typeof(GameBehaviourState)] = new GameBehaviourState(this),
                [typeof(InitialState)] = new InitialState(this, _diContainer.GetService<IFactory>())
            };
        }
    
        public void Enter<TState>() where TState : IState
        {
            _currentState?.Exit();
            var state = _states[typeof(TState)];
            _currentState = state;
            _currentState.Enter();
        }
    
        public void Exit<TState>() where TState : IState
        {
        
        }
    }
}