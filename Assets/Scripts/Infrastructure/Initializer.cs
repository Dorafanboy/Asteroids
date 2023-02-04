using System;
using UnityEngine;

namespace Infrastructure
{
    public class Initializer : MonoBehaviour, ICoroutineRunner, IUpdatable
    {
        public event Action<float> Updated;
        
        private void Awake()
        {
            var game = new Game(this, this);
            
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            Updated?.Invoke(Time.deltaTime);
        }
    }
}