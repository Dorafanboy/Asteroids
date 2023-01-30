using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Infrastructure.States
{
    public class GameBehaviourState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly IEnumerable<IUpdateListener> _updateListeners;

        public GameBehaviourState(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            //_updateListeners = updateListeners;
        }
        
        public void Enter()
        {
            //EnableUpdateListeners();
            _stateMachine.Enter<InitialState>();

            //ChangeStateUpdateListeners(_updateListeners.Select(x => x).First().Enable);
        }

        public void Exit()
        {
            //DisableUpdateListeners();
            //ChangeStateUpdateListeners(_updateListeners.Select(x => x).First().Disable);
        }

        private void ChangeStateUpdateListeners(Action action)
        {
            foreach (var listener in _updateListeners)
            {
                action.Invoke();
            }
        }

        private void EnableUpdateListeners()
        {
            foreach (var listener in _updateListeners)
            {
                listener.Enable();
            }
        }
        
        private void DisableUpdateListeners()
        {
            foreach (var listener in _updateListeners)
            {
                listener.Disable();
            }
        }
    }
}