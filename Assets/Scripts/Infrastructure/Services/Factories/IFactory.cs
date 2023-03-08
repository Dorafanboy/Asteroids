using Entities.Enemy;
using Entities.Guns;
using Infrastructure.Wrapper;
using ShipContent;
using UnityEngine;

namespace Infrastructure.Services.Factories
{
    public interface IFactory : IService
    {
        Ship CreateShip<T, TT>(T firstWeapon, TT secondWeapon) where T : Weapon<Bullet> where TT : Weapon<Bullet>;
        ScreenWrapper CreateWrapper(Ship ship);
        ProjectileWeapon CreateProjectileWeapon(GunType gunType);
        LaserWeapon CreateLaserWeapon(GunType gunType);
        EnemySpawner CreateEnemySpawner(Transform prefabTransform);
        Ufo CreateUfo(Transform playerShip);
        Asteroid CreateAsteroid(Transform playerShip);
    }
}