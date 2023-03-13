using Entities.Guns;
using Infrastructure.Services.Inputs;
using UnityEngine;

namespace Entities.Ship
{
    public class ShipModel
    {
        public float Acceleration { get; }
        public float Deceleration { get;  }
        public float MaxSpeed { get; }
        public float RotationSpeed { get; }
        public GameObject Prefab { get; }
        public IInputService InputService { get; }
        public float CurrentSpeed { get; set; }
        public WeaponBase<Bullet> FirstWeapon { get; }
        public WeaponBase<Bullet> SecondWeapon { get; }

        public ShipModel(float acceleration, float deceleration, float maxSpeed, float rotationSpeed, GameObject prefab, 
            IInputService inputService, WeaponBase<Bullet> firstWeapon, WeaponBase<Bullet> secondWeapon)
        {
            Acceleration = acceleration;
            Deceleration = deceleration;
            MaxSpeed = maxSpeed;
            RotationSpeed = rotationSpeed;
            Prefab = prefab;
            InputService = inputService;
            FirstWeapon = firstWeapon;
            SecondWeapon = secondWeapon;
        }
    }
}