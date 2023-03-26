using Infrastructure;
using Infrastructure.Services.Clashes;
using UnityEngine;

namespace Entities.Enemy
{
    public class Ufo : EnemyEntityBase
    {
        private readonly Transform _playerShip;

        public Ufo(GameObject prefab, float speed, IUpdatable updatable, CollisionType collisionType,
            Transform playerShip) 
            : base(prefab, speed, updatable, collisionType)
        {
            _playerShip = playerShip;
        }

        public override void OnUpdated(float time)
        {
            var nextPosition = Vector3.MoveTowards(Prefab.gameObject.transform.position,
                _playerShip.transform.position, time * Speed);
            
           InstallPosition(nextPosition); 
        }
    }
}