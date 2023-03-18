using System.Collections.Generic;
using Constants;
using Entities.Guns;
using UnityEngine;

namespace Infrastructure.Services.Clashes
{
    public class CollisionHandler : IEventListener
    {
        private readonly List<CollisionChecker> _collisionCheckers;
        private readonly TransformableContainer _transformableContainer;

        public CollisionHandler(TransformableContainer transformableContainer)
        {
            _collisionCheckers = new List<CollisionChecker>();
            for (int i = 0; i < transformableContainer.Listeners.Count; i++)
            {
                _collisionCheckers.Add(transformableContainer.Listeners[i]);
            }

            _transformableContainer = transformableContainer;
        }
        
        public void Enable()
        {
            _transformableContainer.Registered += OnRegistered;
            for (int i = 0; i < _collisionCheckers.Count; i++)
            {
                _collisionCheckers[i].Collided += OnCollided;
            } 
        }

        private void OnRegistered(CollisionChecker obj)
        {
            _collisionCheckers.Add(obj);
            obj.Collided += OnCollided;
        }

        public void Disable()
        {
            _transformableContainer.Registered -= OnRegistered;
            for (int i = 0; i < _collisionCheckers.Count; i++)
            {
                _collisionCheckers[i].Collided -= OnCollided;
            } 
        }

        private void OnCollided(CollisionChecker arg1, GameObject arg2)
        {
            _collisionCheckers.Add(arg1);

            if (arg1.transform.CompareTag(Tags.Enemy) && arg2.CompareTag(Tags.Bullet))
            {
                Debug.Log("Destroy enemy ship");
                Debug.Log(arg1);
                return;
            }

            if (arg1.transform.CompareTag(Tags.Enemy) && arg2.CompareTag(Tags.Bullet))
            {
                Debug.Log("Destroy player ship");
            }
        }
    }
}