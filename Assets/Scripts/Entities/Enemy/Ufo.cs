using Infrastructure;
using UnityEngine;

namespace Entities.Enemy
{
    public class Ufo : EnemyEntityBase
    {
        private readonly Transform _playerShip;

        public Ufo(GameObject prefab, float speed, Transform playerShip, IUpdatable updatable) : base(prefab, speed, updatable)
        {
            _playerShip = playerShip;
        }

        public override void OnUpdated(float time)
        {
            Prefab.gameObject.transform.position = Vector3.MoveTowards(Prefab.gameObject.transform.position,
                _playerShip.transform.position, time * Speed);
        }
    }
}