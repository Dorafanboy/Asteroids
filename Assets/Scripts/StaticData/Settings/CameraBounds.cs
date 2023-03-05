using System;
using UnityEngine;

namespace StaticData.Settings
{
    [Serializable]
    public class CameraBounds
    {
        [SerializeField] private Vector3 _spawnPositions;

        public Vector3 SpawnPositions => _spawnPositions;
    }
}