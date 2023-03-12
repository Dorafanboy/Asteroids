using Infrastructure;
using UnityEngine;

namespace Entities.Enemy
{
    public class Asteroid : EnemyEntityBase
    {
        private readonly Camera _camera;
        private Vector3 _targetPosition;

        public Asteroid(GameObject prefab, float speed, IUpdatable updatable, Camera camera) : base(prefab, speed,
            updatable)
        {
            _camera = camera;
        }
        
        public override void OnUpdated(float time)
        {
            Prefab.gameObject.transform.position = Vector3.MoveTowards(Prefab.gameObject.transform.position,
                _targetPosition, Speed * time);
        
            if (Prefab.gameObject.transform.position == _targetPosition)
            {
                _targetPosition = GetWorldPoint();
            }
        }

        private Vector3 GetWorldPoint()
        {
            var position = _camera.ScreenToWorldPoint(GetTargetPosition());
            position.z = 0;
            
            return position;
        }

        private Vector3 GetTargetPosition()
        {
            return new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 0);
        }
    }
}