using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "PoolData", menuName = "NewData/PoolData")]
    public class PoolStaticData : ScriptableObject
    {
        [SerializeField] private int _poolSize;
    
        public int PoolSize => _poolSize;
    }
}