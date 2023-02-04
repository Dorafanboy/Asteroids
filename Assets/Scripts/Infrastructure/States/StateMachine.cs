using System;
using System.Collections.Generic;
using Infrastructure.Loaders;
using Infrastructure.Services.Containers;
using Infrastructure.Services.Factories;

namespace Infrastructure.States
{
    public class StateMachine : IStateMachine
    {
        private readonly IDiContainer _diContainer;
        
        private readonly Dictionary<Type, IState> _states;
        private IState _currentState;

        public StateMachine(ISceneLoader sceneLoader, IDiContainer container, IUpdatable updatable)
        {
            _diContainer = container;
            
            _states = new Dictionary<Type, IState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, _diContainer, sceneLoader, updatable),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader),
                [typeof(InitialState)] = new InitialState(this, _diContainer.GetService<IFactory>()),
                [typeof(GameBehaviourState)] = new GameBehaviourState(this)
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