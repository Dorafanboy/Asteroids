using Entities.Guns;
using Infrastructure.Services.Inputs;
using UnityEngine;

namespace ShipContent
{
    public class Ship
    {
        private float _shotCooldown;
        private int _maxAmmo;
        public float MaxSpeed { get; }
        public float Deceleration { get;  }
        public float Acceleration { get; }
        public float RotationSpeed { get; }
        public GameObject Prefab { get; }
        public IInputService InputService { get; }
        public float CurrentSpeed { get; set; }
        public Weapon<Bullet> FirstWeapon { get; }
        public Weapon<Bullet> SecondWeapon { get; }

        public Ship(float acceleration, float deceleration, float maxSpeed, float rotationSpeed,
            float shotCooldown, int maxAmmo, GameObject prefab, IInputService inputService, 
            Weapon<Bullet> firstWeapon, Weapon<Bullet> secondWeapon)
        {
            Acceleration = acceleration;
            Deceleration = deceleration;
            MaxSpeed = maxSpeed;
            RotationSpeed = rotationSpeed;
            _shotCooldown = shotCooldown;
            _maxAmmo = maxAmmo;
            Prefab = prefab;
            InputService = inputService;
            FirstWeapon = firstWeapon;
            SecondWeapon = secondWeapon;
        }
    }
}

public interface IShip<out T, out TT> where T : IWeapon<Bullet> where TT : IWeapon<Bullet>
{
    T FirstWeapon { get; }
    TT SecondWeapon { get; }
}