using Entities.Guns;
using Infrastructure;
using UnityEngine;

namespace Entities.Ship
{
    public class ShipPresenter : IEventListener 
    {
        private readonly ShipModel _shipModel;
        private readonly ShipView _view;

        public ShipPresenter(ShipModel shipModel, ShipView view)
        {
            _shipModel = shipModel;
            _view = view;
            
            Enable();
        }
        
        public void Enable()    
        {
            _shipModel.InputService.MoveKeyDowned += OnMovedKeyDowned;
            _shipModel.InputService.RotateKeyDowned += OnRotateKeyDowned;
            
            _shipModel.InputService.FirstWeaponFired += OnFirstWeaponFired;
            _shipModel.InputService.SecondWeaponFired += OnSecondWeaponFired;
        }

        public void Disable()
        {
            _shipModel.InputService.MoveKeyDowned -= OnMovedKeyDowned;
            _shipModel.InputService.RotateKeyDowned -= OnRotateKeyDowned;
            
            _shipModel.InputService.FirstWeaponFired -= OnFirstWeaponFired;
            _shipModel.InputService.SecondWeaponFired -= OnSecondWeaponFired;
        }

        private void OnRotateKeyDowned(float direction, float time)    
        {
            var angle = _shipModel.Prefab.transform.eulerAngles.z + time * direction * _shipModel.RotationSpeed;
            
            _view.InstallAngleRotation(angle);
        }

        private void OnMovedKeyDowned(Vector2 direction, float time)
        {
            var speedAffect = direction != Vector2.zero ? _shipModel.Acceleration : -_shipModel.Deceleration;
            
            _shipModel.CurrentSpeed += speedAffect * time;

            _shipModel.CurrentSpeed = Mathf.Clamp(_shipModel.CurrentSpeed, 0, _shipModel.MaxSpeed);

            var shipPosition = _shipModel.Prefab.transform.position + _shipModel.Prefab.transform.up * _shipModel.CurrentSpeed;
            _view.InstallPosition(shipPosition);
        }

        private void OnFirstWeaponFired()
        {
            Shoot(_shipModel.FirstWeapon);
        }

        private void OnSecondWeaponFired()
        {
            Shoot(_shipModel.SecondWeapon);
        }

        private void Shoot(IWeapon<Bullet> weapon)
        {
            var angle = Quaternion.Euler(0, 0, _shipModel.Prefab.transform.eulerAngles.z);
            
            weapon.Shoot(_shipModel.Prefab.transform.position, angle);
        }
    }
}