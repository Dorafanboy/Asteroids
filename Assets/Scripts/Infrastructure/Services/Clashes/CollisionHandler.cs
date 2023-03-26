using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Services.Clashes
{
    public class CollisionHandler : IEventListener
    {
        private readonly List<CollisionChecker>  _collisionActors;
        private readonly TransformableContainer _transformableContainer;

        public event Action<CollisionChecker, bool> UfoDestroyedByLaser;
        
        public CollisionHandler(TransformableContainer transformableContainer)
        {
            _collisionActors = new List<CollisionChecker>();
            _collisionActors = (List<CollisionChecker>)transformableContainer.Listeners;
            _transformableContainer = transformableContainer;
        }

        public void Enable()
        {
            _transformableContainer.Registered += OnRegistered;

            for (int i = 0; i < _collisionActors.Count; i++)
            {
                _collisionActors[i].Collided += OnCollided;
            }
        }

        public void Disable()
        {
            _transformableContainer.Registered -= OnRegistered;

            for (int i = 0; i < _collisionActors.Count; i++)
            {
                _collisionActors[i].Collided -= OnCollided;
            }
        }

        private void OnRegistered(CollisionChecker checker)
        {
            _collisionActors.Add(checker);
            checker.Collided += OnCollided;
        }

        private void OnCollided(CollisionChecker arg1, CollisionChecker arg2)
        {
            switch (arg1.CollisionType)
            {
                case CollisionType.Projectile when IsEnemyShip(arg2):
                    DisableCollidedObjects(arg1, arg2);
                    UfoDestroyedByLaser?.Invoke(arg2, true);
                    return;
                case CollisionType.Laser when IsEnemyShip(arg2):
                    DisableCollidedObjects(arg1, arg2);
                    UfoDestroyedByLaser?.Invoke(arg2, false);
                    return;
            }
            
            if (IsEnemyShip(arg1) && arg2.CollisionType == CollisionType.Player)
            {
                DisableCollidedObjects(arg1, arg2);
                UfoDestroyedByLaser?.Invoke(arg1, false);
            }
        }

        private void DisableCollidedObjects(CollisionChecker arg1, CollisionChecker arg2)
        {
            arg1.gameObject.SetActive(false);
            arg2.gameObject.SetActive(false);
        }

        private bool IsEnemyShip(CollisionChecker arg2)
        {
            return arg2.CollisionType == CollisionType.Asteroid || arg2.CollisionType == CollisionType.Ufo;
        }
    }
}