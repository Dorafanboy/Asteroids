using Infrastructure;
using UnityEngine;

namespace Entities.Guns
{
    public class Bullet : IUpdateListener, ITransformable
    {
        private readonly float _deceleration;
        private readonly IUpdatable _updatable;
        public GameObject Prefab { get; }

        public Bullet(float deceleration, IUpdatable updatable, GameObject prefab)
        {
            _deceleration = deceleration;
            _updatable = updatable;
            Prefab = prefab; 
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
            var position = Prefab.transform.position + time * Prefab.transform.up * _deceleration;
            InstallPosition(position);
        }

        public void InstallPosition(Vector3 position)
        {
            Prefab.transform.position = position;
        }
    }
}