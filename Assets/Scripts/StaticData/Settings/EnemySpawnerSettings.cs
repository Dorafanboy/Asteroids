using UnityEngine;

namespace StaticData.Settings
{
    [CreateAssetMenu(fileName = "EnemySpawnerSettings", menuName = "Create Settings/EnemySpawnerSettings")]
    public class EnemySpawnerSettings : ScriptableObject
    {
        [SerializeField] private int _spawnDelay;
        [SerializeField] private int _enemyCount;
        [SerializeField] private int _sideCount;

        public int SpawnDelay => _spawnDelay;
        public int EnemyCount => _enemyCount;
        public int SideCount => _sideCount;
    }
}