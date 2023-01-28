using UnityEngine;

namespace ShipContent
{
    public class Ship
    {
        private float _acceleration;
        private float _deceleration;
        private float _maxSpeed;
        private float _shotCooldown;
        private int _maxAmmo;

        public float RotationSpeed { get; }
        public GameObject Prefab { get; }

        public Ship(float acceleration, float deceleration, float maxSpeed, float rotationSpeed, float shotCooldown, int maxAmmo, GameObject prefab)
        {
            _acceleration = acceleration;
            _deceleration = deceleration;
            _maxSpeed = maxSpeed;
            RotationSpeed = rotationSpeed;
            _shotCooldown = shotCooldown;
            _maxAmmo = maxAmmo;
            Prefab = prefab;
        }
    }
}