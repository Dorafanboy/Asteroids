using System;
using Infrastructure.Loaders;
using Infrastructure.States;
using UnityEngine;

namespace Infrastructure
{
    public class Initializer : MonoBehaviour, ICoroutineRunner, IUpdatable
    {
        public event Action<float> Updated;
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
            var sceneLoader = new SceneLoader(this);
            var stateMachine = new StateMachine(sceneLoader,this, this);
            
            stateMachine.Enter<BootstrapState>();
        }

        private void Update()
        {
            Updated?.Invoke(Time.deltaTime);
        }
    }
}