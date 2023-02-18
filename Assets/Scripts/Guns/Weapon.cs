﻿using System;
using Guns;
using Infrastructure;
using Infrastructure.Services.Factories;
using Infrastructure.Wrapper;
using UnityEngine;

namespace Guns
{
    public class Weapon<T> : IWeapon<T> where T : Bullet
    {
        private readonly ObjectPool<T> _objectPool;
        private readonly BulletScreenWrapper<T> _bulletWrapper;
        private readonly GunType _gunType;
        public event Action<T> Shooted;

        public Weapon(Func<GunType, T> factory, IUpdatable updatable, GunType gunType)
        {
            _objectPool = new ObjectPool<T>(10, factory);
            _bulletWrapper = new BulletScreenWrapper<T>(updatable, _objectPool, this);
            _gunType = gunType;
        }

        public void Shoot(Vector3 position, Quaternion angle)
        {
            var bullet = _objectPool.GetObject(_gunType);

            bullet.Prefab.transform.position = position;
            bullet.Prefab.transform.eulerAngles = angle.eulerAngles;
            bullet.Prefab.SetActive(true);
            
            Shooted?.Invoke(bullet);
        }

        public Type GetBulletType()
        {
            return typeof(T);
        }
    }
}

