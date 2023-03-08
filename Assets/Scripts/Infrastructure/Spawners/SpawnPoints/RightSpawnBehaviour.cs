using UnityEngine;

namespace Infrastructure.Spawners.SpawnPoints
{
    public readonly struct RightSpawnBehaviour : ISpawnBehaviour
    {
        public float Position { get; }

        public RightSpawnBehaviour(float position)
        {
            Position = position;
        }

        public Vector3 GetSpawnPosition()
        {
            return new Vector3(Position, Random.Range(0f, 1f), 0);
        }
    }
}