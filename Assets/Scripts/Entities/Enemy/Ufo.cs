using Infrastructure;
using UnityEngine;

namespace Entities.Enemy
{
    public class Ufo : EnemyEntityBase, IUpdateListener
    {
        private readonly Transform _playerShip;
        private readonly IUpdatable _updatable;

        public Ufo(GameObject prefab, float speed, Transform playerShip, IUpdatable updatable) : base(prefab, speed)
        {
            _playerShip = playerShip;
            _updatable = updatable;

            Enable();
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
            Prefab.gameObject.transform.position = Vector3.MoveTowards(Prefab.gameObject.transform.position,
                _playerShip.transform.position, time * Speed);
        }
    }
}