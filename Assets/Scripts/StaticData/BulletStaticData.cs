using Entities.Guns;
using UnityEngine;
using UnityEngine.Serialization;

namespace StaticData
{
    [CreateAssetMenu(fileName = "BulletData", menuName = "New Data/BulletData")]
    public class BulletStaticData : ScriptableObject
    {
        [FormerlySerializedAs("_gunType")] [SerializeField] private BulletType bulletType;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private float _deceleration;
        [SerializeField] private float _fireCooldown;
        
        public BulletType BulletType => bulletType;
        public GameObject Prefab => _prefab;
        public float Deceleration => _deceleration;
        public float FireCooldown => _fireCooldown;
    }
}