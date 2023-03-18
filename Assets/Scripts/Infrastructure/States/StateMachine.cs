using System;
using System.Collections.Generic;
using Infrastructure.Loaders;
using Infrastructure.Services.Containers;
using UnityEngine;

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
            var cont = new EventListenerContainer();
            var transform = new TransformableContainer();
            var camera = Camera.main;
            
            _states = new Dictionary<Type, IState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, _diContainer, sceneLoader, updatable, cont, camera, transform),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader),
                [typeof(InitialState)] = new InitialState(this, _diContainer),
                [typeof(GameBehaviourState)] = new GameBehaviourState(this, cont, transform)
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