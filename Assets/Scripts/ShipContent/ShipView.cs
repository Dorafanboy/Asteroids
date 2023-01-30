using System;
using Infrastructure.Services.Inputs;
using UnityEngine;

namespace ShipContent
{
    public class ShipView
    {
        private readonly Ship _ship;
        private readonly IInputService _inputService;
        private float _speed = 0f;
        
        public event Action<Vector3> ShipMoved;
        public event Action<float> ShipRotated;
        
        public ShipView(Ship ship, IInputService inputService)
        {
            _ship = ship;
            _inputService = inputService;
            
            _inputService.RotateKeyDowned += OnRotateKeyDowned;
            _inputService.MoveKeyDowned += OnMovedKeyDowned;
            _inputService.MoveKeyUpped += OnMovedKeyUpped;
        }

        private void OnRotateKeyDowned(float time)
        {
            var angle = _ship.Prefab.transform.eulerAngles.z + time;
            ShipRotated?.Invoke(angle);
            _ship.Prefab.transform.rotation = Quaternion.Lerp(_ship.Prefab.transform.rotation,
                Quaternion.Euler(0, 0, angle), Time.deltaTime * _ship.RotationSpeed);
        }

        private void OnMovedKeyDowned(bool isDowned, float time)
        {
            if (isDowned)
            {
                _speed += _ship.Acceleration * time;
            }
            else
            {                
                _speed -= _ship.Deceleration * time;
            }
            
            _speed = Mathf.Clamp(_speed, 0, _ship.MaxSpeed);
            _ship.Prefab.transform.position += _ship.Prefab.transform.up * _speed * time;
        }

        private void OnMovedKeyUpped(float obj)
        {
            // speed = Mathf.Lerp(speed, isDowned ? _ship.Acceleration * _ship.MaxSpeed 
            //         : _ship.Deceleration *_ship.MaxSpeed, time);
            //speed = Mathf.Lerp(speed, _ship.Deceleration * _ship.MaxSpeed,time);
            //_speed = Mathf.Lerp(_speed, _ship.Acceleration * _ship.MaxSpeed,time);
            var speed = Mathf.Min(obj * _ship.Deceleration, _ship.MaxSpeed);
            _ship.Prefab.transform.position += _ship.Prefab.transform.up * speed;
        }
    }
}