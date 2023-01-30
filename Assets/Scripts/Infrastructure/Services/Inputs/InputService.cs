using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Services.Inputs
{
    public class InputService : IInputService, IUpdateListener
    {
        private readonly IUpdatable _updatable;
        private readonly List<InputKey> _keyInputs;
        
        public event Action<float> RotateKeyDowned;
        public event Action<bool, float> MoveKeyDowned;
        public event Action<float> MoveKeyUpped;

        public InputService(IUpdatable updatable)
        {
            _updatable = updatable;
            _keyInputs = new List<InputKey>();
            
            Enable();
        }
        
        public void Enable()
        {
            _updatable.Updated += OnUpdated;

            _keyInputs.Add(new InputKey(KeyCode.W, 1f, true));
            _keyInputs.Add(new InputKey(KeyCode.A, 1f, false));
            _keyInputs.Add(new InputKey(KeyCode.D, -1f, false));
        }

        public void Disable()
        {
            _updatable.Updated -= OnUpdated;
        }

        public void OnUpdated(float time)
        {
            foreach(var inputKey in _keyInputs)
            {
                if (inputKey.IsKeyMoved && Input.GetKey(inputKey.Key))
                {
                    MoveKeyDowned?.Invoke(true, time);
                    return;
                }

                if(Input.GetKey(inputKey.Key))
                {
                    RotateKeyDowned?.Invoke(inputKey.Value * time * 1000);
                    MoveKeyUpped?.Invoke(time);
                    return;
                }

                MoveKeyDowned?.Invoke(false, time);
                // MoveKeyUpped?.Invoke(time);
            }
        }
    }
}

public class InputKey
{
    public KeyCode Key { get; }
    public float Value { get; }
    public bool IsKeyMoved { get; }

    public InputKey(KeyCode key, float value, bool isKeyMoved)
    {
        Key = key;
        Value = value;
        IsKeyMoved = isKeyMoved;
    }
}