using Entities.Guns;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "BulletData", menuName = "New Data/BulletData")]
    public class BulletStaticData : ScriptableObject
    {
        [SerializeField] private GunType _gunType;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private float _deceleration;
        [SerializeField] private float _fireCooldown;
        [SerializeField] private int _shotsCount;
        
        public GunType GunType => _gunType;
        public GameObject Prefab => _prefab;
        public float Deceleration => _deceleration;
        public float FireCooldown => _fireCooldown;
        public int ShotsCount => _shotsCount;
    }
}