using Infrastructure;
using UnityEngine;

namespace Entities.Enemy
{
    public class Asteroid : EnemyEntityBase, IUpdateListener
    {
        private readonly IUpdatable _updatable;
        private readonly Camera _camera;
        
        private Vector3 _targetPosition;

        public Asteroid(GameObject prefab, float speed, IUpdatable updatable, Camera camera) : base(prefab, speed)
        {
            _updatable = updatable;
            _camera = camera;

            //Enable();
        }

        public void Enable()
        {
            _updatable.Updated += OnUpdated;
            
            _targetPosition = GetWorldPoint();
        }

        public void Disable()
        {
            _updatable.Updated -= OnUpdated;
        }

        public void OnUpdated(float time)
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
            var position = _camera.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width),
                Random.Range(0, Screen.height), 0));
            position.z = 0;
            
            return position;
        }
    }
}