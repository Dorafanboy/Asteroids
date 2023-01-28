using System;
using Infrastructure.Services.Inputs;
using UnityEngine;

namespace ShipContent
{
    public class ShipView
    {
        private readonly Ship _ship;
        private readonly IInputService _inputService;
        
        public event Action<Vector3> ShipMoved;
        public event Action<float> ShipRotated;
        
        public ShipView(Ship ship, IInputService inputService)
        {
            _ship = ship;
            _inputService = inputService;
            _inputService.KeyDowned += OnKeyDowned;
        }

        private void OnKeyDowned(float time)
        {
            var angle = _ship.Prefab.transform.eulerAngles.z + time;
            ShipRotated?.Invoke(angle);
            _ship.Prefab.transform.rotation = Quaternion.Lerp(_ship.Prefab.transform.rotation,
                Quaternion.Euler(0, 0, angle), Time.deltaTime * _ship.RotationSpeed);
        }
    }
}