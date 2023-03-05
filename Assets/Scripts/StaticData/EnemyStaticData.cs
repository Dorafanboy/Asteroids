using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "EnemyStaticData", menuName = "New Data/EnemyStaticData")]
    public class EnemyStaticData : ScriptableObject
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private float _speed;

        public GameObject Prefab => _prefab;
        public float Speed => _speed;
    }
}