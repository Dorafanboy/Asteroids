using System.Collections.Generic;
using Infrastructure;
using Infrastructure.Services.Clashes;
using UnityEngine;

namespace Entities.Enemy
{
    public class Asteroid : EnemyEntityBase
    {
        private readonly Camera _camera;
        private readonly List<Asteroid> _asteroids;
        private Vector3 _targetPosition;
        public bool IsSmall { get; private set; }

        public Asteroid(GameObject prefab, float speed, IUpdatable updatable, CollisionType collisionType, Camera camera, 
            bool isSmall) : base(prefab, speed, updatable, collisionType)
        {
            _camera = camera;
            IsSmall = isSmall;
            _asteroids = new List<Asteroid>();
        }

        public override void OnUpdated(float time)
        {
            var nextPosition = Vector3.MoveTowards(Prefab.gameObject.transform.position,
                _targetPosition, Speed * time);
            InstallPosition(nextPosition);
        
            if (Prefab.gameObject.transform.position == _targetPosition)
            {
                _targetPosition = GetWorldPoint();
            }
        }

        public void Add(Asteroid miniAsteroid)
        {
            _asteroids.Add(miniAsteroid);
        }

        public List<Asteroid> GetAsteroids()
        {
            return _asteroids;
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