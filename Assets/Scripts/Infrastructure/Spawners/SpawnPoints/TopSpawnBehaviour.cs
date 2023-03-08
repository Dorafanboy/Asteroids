using UnityEngine;

namespace Infrastructure.Spawners.SpawnPoints
{
    public readonly struct TopSpawnBehaviour : ISpawnBehaviour
    {
        public float Position { get; }

        public TopSpawnBehaviour(float position)
        {
            Position = position;
        }

        public Vector3 GetSpawnPosition()
        {
            return new Vector3(Random.Range(0f, 1f), Position, 0);
        }
    }
}