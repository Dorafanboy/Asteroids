using Entities.Pool;
using Infrastructure;
using UnityEngine;

namespace Entities.Guns
{
    public class LaserWeapon : WeaponBase<Bullet>, IUpdateListener
    {
        private readonly IUpdatable _updatable;
        private readonly float _bulletCooldown;
        private float _currentWait;
        private int _remainingShots;
    
        public LaserWeapon(ObjectPool<Bullet> objectPool, IUpdatable updatable, GunType gunType, float bulletCooldown) 
            : base(objectPool, gunType)
        {
            _updatable = updatable; 
            _bulletCooldown = bulletCooldown;
            _currentWait = _bulletCooldown;
        }
    
        public void Enable()
        {
            _updatable.Updated += OnUpdated;
        }

        public void Disable()
        {
            _updatable.Updated -= OnUpdated;
        }

        public void OnUpdated(float time)
        {
            if (_currentWait <= 0)
            {
                UpdateShotCount();
            }
        
            _currentWait -= time;
        }

        public override void Shoot(Vector3 position, Quaternion angle)
        {
            if (CanShoot())
            {
                base.Shoot(position, angle);
                _remainingShots--;
            }
        }

        private void UpdateShotCount()
        {
            _currentWait = _bulletCooldown;
            _remainingShots++;
        }

        private bool CanShoot()
        {
            return _remainingShots > 0;
        }
    }
}