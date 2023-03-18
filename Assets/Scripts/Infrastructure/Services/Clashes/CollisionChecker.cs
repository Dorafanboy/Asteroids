using System;
using UnityEngine;

namespace Infrastructure.Services.Clashes
{
    public class CollisionChecker : MonoBehaviour, IService
    {
        public event Action<CollisionChecker, GameObject> Collided;
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            Collided?.Invoke(this, col.gameObject);
        }
    }
}