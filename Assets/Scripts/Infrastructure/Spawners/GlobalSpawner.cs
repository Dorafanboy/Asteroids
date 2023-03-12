using System.Collections.Generic;
using StaticData.Settings;

namespace Infrastructure.Spawners
{
    public class GlobalSpawner : IUpdateListener
    {
        private readonly Dictionary<EnemySpawnerSettings, ISpawner> _spawners;
        private readonly IUpdatable _updatable;

        public GlobalSpawner(Dictionary<EnemySpawnerSettings, ISpawner> spawners, IUpdatable updatable)
        {
            _spawners = spawners;
            _updatable = updatable;
        }

        public void Enable()
        {
            _updatable.Updated += OnUpdated;
        }

        public void Disable()
        {
            _updatable.Updated -= OnUpdated;
        }

        public void OnUpdated(float time)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface ISpawner
    {
        void Spawn();
    }
}