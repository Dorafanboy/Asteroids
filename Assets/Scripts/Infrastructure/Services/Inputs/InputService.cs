using System;
using UnityEngine;

namespace Infrastructure.Services.Inputs
{
    public class InputService : IInputService, IUpdateListener
    {
        public event Action<Vector2, float> MoveKeyDowned;
        public event Action<float, float> RotateKeyDowned;
        public event Action ShootKeyDowned;

        private readonly IUpdatable _updatable;
        private readonly PlayerInput _playerInput;

        public InputService(IUpdatable updatable, PlayerInput playerInput)
        {
            _updatable = updatable;
            _playerInput = playerInput;
            
            _playerInput.Gameplay.Shoot.performed += ctx => OnShoot();
            
            Enable();
        }
        
        public void Enable()
        {
            _updatable.Updated += OnUpdated;
            _playerInput.Enable();
        }

        public void Disable()
        {
            _updatable.Updated -= OnUpdated;
        }

        public void OnUpdated(float time)
        {
            var move = _playerInput.Gameplay.Movement.ReadValue<Vector2>();
            var rotate = _playerInput.Gameplay.Rotate.ReadValue<float>();

            MoveKeyDowned?.Invoke(move, time);
            RotateKeyDowned?.Invoke(rotate, time);
        }

        private void OnShoot()
        {
            ShootKeyDowned?.Invoke();
        }
    }
}
