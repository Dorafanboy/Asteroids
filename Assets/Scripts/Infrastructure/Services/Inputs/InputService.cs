using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Services.Inputs
{
    public class InputService : IInputService, IUpdateListener
    {
        private readonly IUpdatable _updatable;
        private readonly Dictionary<KeyCode, float> _inputs;
        
        public event Action<float> KeyDowned;

        public InputService(IUpdatable updatable)
        {
            _updatable = updatable;
            _inputs = new Dictionary<KeyCode, float>();
        }
        
        public void Enable()
        {
            _updatable.Updated += OnUpdated;
            _inputs.Add(KeyCode.A, 1f);
            _inputs.Add(KeyCode.D, -1f);
        }

        public void Disable()
        {
            _updatable.Updated -= OnUpdated;
        }

        public void OnUpdated(float time)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("Moved");
                //ShipMoved?.Invoke(Vector3.zero);
            }

            foreach(var input in _inputs)
            {
                if (Input.GetKey(input.Key))
                {
                    KeyDowned?.Invoke(input.Value * time * 1000);
                }
            }
        }
    }
}