using UnityEngine;

namespace Infrastructure.Spawners.SpawnPoints
{
    public interface ISpawnBehaviour
    {
        float Position { get; }
        Vector3 GetSpawnPosition();
    }
}