using Infrastructure;
using UnityEngine;

namespace ShipContent
{
    public class ShipPresenter : IEventListener
    {
        private readonly Ship _ship;
        private readonly ShipView _view;
        private float _speed;

        public ShipPresenter(Ship ship, ShipView view)
        {
            _ship = ship;
            _view = view;
            
            Enable();
        }
        
        public void Enable()
        {
            _ship.InputService.MoveKeyDowned += OnMovedKeyDowned;
            _ship.InputService.RotateKeyDowned += OnRotateKeyDowned;
        }

        public void Disable()
        {
            _ship.InputService.MoveKeyDowned -= OnMovedKeyDowned;
            _ship.InputService.RotateKeyDowned -= OnRotateKeyDowned;
        }
        
        private void OnRotateKeyDowned(float direction, float time)    
        {
            var angle = _ship.Prefab.transform.eulerAngles.z + time * direction * _ship.RotationSpeed;
            
            _view.InstallAngleRotation(angle);
        }
        
        private void OnMovedKeyDowned(Vector2 direction, float time)
        {
            var speedAffect = direction != Vector2.zero ? _ship.Acceleration : -_ship.Deceleration;
            
            _speed += speedAffect * time;

            _speed = Mathf.Clamp(_speed, 0, _ship.MaxSpeed);
            _view.InstallPosition(_ship.Prefab.transform.up * _speed);
        }
    }
}