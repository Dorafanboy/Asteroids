using Guns;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "BulletData", menuName = "NewData/BulletData")]
    public class BulletStaticData : ScriptableObject
    {
        [SerializeField] private GunType _gunType;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private float _deceleration;

        public GunType GunType => _gunType;
        public GameObject Prefab => _prefab;
        public float Deceleration => _deceleration;
    }
}