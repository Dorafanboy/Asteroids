using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Spawners.SpawnPoints
{
    public readonly struct SpawnPointsContainer
    {
        private readonly List<ISpawnBehaviour> _spawns;

        public SpawnPointsContainer(List<ISpawnBehaviour> vectors)
        {
            _spawns = vectors;
        }

        public Vector3 GetSpawnPosition(int idx)
        {
            return _spawns[idx].GetSpawnPosition();
        }
    }
}