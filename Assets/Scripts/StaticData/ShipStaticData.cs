using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "ShipData", menuName = "New Data/ShipData")]
    public class ShipStaticData : ScriptableObject
    {
        [SerializeField] private float _acceleration;
        [SerializeField] private float _deceleration;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _shotCooldown;
        [Range(0, 3)]
        [SerializeField] private int _maxAmmo;
        [SerializeField] private GameObject _prefab;
        
        public float Acceleration => _acceleration;
        public float Deceleration => _deceleration;
        public float MaxSpeed => _maxSpeed;
        public float RotationSpeed => _rotationSpeed;
        public float ShotCooldown => _shotCooldown;
        public int MaxAmmo => _maxAmmo;
        public GameObject Prefab => _prefab;
    }
}