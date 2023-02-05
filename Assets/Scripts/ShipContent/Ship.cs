using Guns;
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
        public IWeapon Weapon { get;}

        public Ship(float acceleration, float deceleration, float maxSpeed, float rotationSpeed, 
            float shotCooldown, int maxAmmo, GameObject prefab, IInputService inputService, IWeapon weapon)
        {
            Acceleration = acceleration;
            Deceleration = deceleration;
            MaxSpeed = maxSpeed;
            RotationSpeed = rotationSpeed;
            _shotCooldown = shotCooldown;
            _maxAmmo = maxAmmo;
            Prefab = prefab;
            InputService = inputService;
            Weapon = weapon;
        }
    }
}