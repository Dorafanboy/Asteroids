using Infrastructure;
using UnityEngine;

namespace ShipContent
{
    public class ShipPresenter : IEventListener
    {
        private readonly Ship _ship;
        private readonly ShipView _view;

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
            _ship.InputService.ShootKeyDowned += OnShootKeyDowned;
        }

        public void Disable()
        {
            _ship.InputService.MoveKeyDowned -= OnMovedKeyDowned;
            _ship.InputService.RotateKeyDowned -= OnRotateKeyDowned;
            _ship.InputService.ShootKeyDowned -= OnShootKeyDowned;
        }

        private void OnRotateKeyDowned(float direction, float time)    
        {
            var angle = _ship.Prefab.transform.eulerAngles.z + time * direction * _ship.RotationSpeed;
            
            _view.InstallAngleRotation(angle);
        }

        private void OnMovedKeyDowned(Vector2 direction, float time)
        {
            var speedAffect = direction != Vector2.zero ? _ship.Acceleration : -_ship.Deceleration;
            
            _ship.CurrentSpeed += speedAffect * time;

            _ship.CurrentSpeed = Mathf.Clamp(_ship.CurrentSpeed, 0, _ship.MaxSpeed);
            _view.InstallPosition(_ship.Prefab.transform.up * _ship.CurrentSpeed);
        }

        private void OnShootKeyDowned()
        {
            var angle = Quaternion.Euler(0, 0, _ship.Prefab.transform.eulerAngles.z);
            
            _ship.Weapon.Shoot(_ship.Prefab.transform.position, angle);
        }
    }
}