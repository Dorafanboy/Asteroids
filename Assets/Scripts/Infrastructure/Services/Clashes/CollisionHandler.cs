using System.Collections.Generic;
using System.Linq;
using Constants;
using Entities.Guns;
using UnityEngine;

namespace Infrastructure.Services.Clashes
{
    public class CollisionHandler : IEventListener
    {
        private readonly List<CollisionActors> _collisionActors;
        private readonly TransformableContainer _transformableContainer;

        public CollisionHandler(TransformableContainer transformableContainer)
        {
            _collisionActors = new List<CollisionActors>();
            _collisionActors = (List<CollisionActors>)transformableContainer.Listeners;
            _transformableContainer = transformableContainer;
        }
        
        public void Enable()
        {
            _transformableContainer.Registered += OnRegistered;
                
            for (int i = 0; i < _collisionActors.Count; i++)
            {
                _collisionActors[i].CollisionChecker.Collided += OnCollided;
            } 
        }

        public void Disable()
        {
            _transformableContainer.Registered -= OnRegistered;
            
            for (int i = 0; i < _collisionActors.Count; i++)
            {
                _collisionActors[i].CollisionChecker.Collided -= OnCollided;
            } 
        }

        private void OnRegistered(CollisionActors actors)
        {
            _collisionActors.Add(actors);
            actors.CollisionChecker.Collided += OnCollided;
        }

        private void OnCollided(CollisionChecker arg1, GameObject arg2)
        {
            if (arg1.transform.CompareTag(Tags.Enemy) && arg2.CompareTag(Tags.Bullet))
            {
                var transformable = _collisionActors
                    .Select(col => col.Transformable)
                    .First(t => t.Prefab == arg1.gameObject);
                
                transformable.DisableObject();
                
                _collisionActors.Add(new CollisionActors(arg1, transformable));
                return;
            }

            if (arg1.transform.CompareTag(Tags.Enemy) && arg2.CompareTag(Tags.Player))
            {
                var transformable = _collisionActors
                    .Select(col => col.Transformable)
                    .First(t => t.Prefab == arg2);
                
                _collisionActors.Add(new CollisionActors(arg1, transformable));

                transformable.DisableObject();
            }
        }
    }
}