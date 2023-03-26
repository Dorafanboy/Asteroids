using System;
using UnityEngine;

namespace Infrastructure.Services.Clashes
{
    public class CollisionChecker : MonoBehaviour, IService, ICollideable
    {
        [SerializeField] private CollisionType _collisionType;
        public CollisionType CollisionType => _collisionType;
        public event Action<CollisionChecker, CollisionChecker> Collided;
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent(out CollisionChecker checker))
            {
                Collided?.Invoke(this, checker);
            }
        }
    }
}