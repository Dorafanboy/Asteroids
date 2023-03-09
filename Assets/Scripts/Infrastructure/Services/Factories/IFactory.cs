﻿using Entities.Enemy;
using Entities.Guns;
using Entities.Ship;
using Infrastructure.Spawners;
using Infrastructure.Wrapper;
using UnityEngine;

namespace Infrastructure.Services.Factories
{
    public interface IFactory : IService
    {
        ShipModel CreateShip<T, TT>(T firstWeapon, TT secondWeapon) where T : Weapon<Bullet> where TT : Weapon<Bullet>;
        ScreenWrapper CreateWrapper(ShipModel shipModel);
        ProjectileWeapon CreateProjectileWeapon(GunType gunType);
        LaserWeapon CreateLaserWeapon(GunType gunType);
        EnemySpawner CreateEnemySpawner(Transform prefabTransform);
        Ufo CreateUfo(Transform playerShip);
        Asteroid CreateAsteroid(Transform playerShip);
    }
}