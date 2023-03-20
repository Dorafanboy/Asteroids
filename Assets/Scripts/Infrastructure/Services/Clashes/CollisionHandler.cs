using System.Collections.Generic;
using System.Linq;
using Constants;
using Entities.Guns;
using Entities.Pool;
using UnityEngine;

namespace Infrastructure.Services.Clashes
{
    public class CollisionHandler : IEventListener
    {
        private readonly List<CollisionActors> _collisionActors;
        private readonly TransformableContainer _transformableContainer;
       // private readonly PoolBase<ITransformable> _poolBase;

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
                    .Select(col => col)
                    .First(t => t.Transformable.Prefab == arg1.gameObject);

                var spaceId = _collisionActors.IndexOf(transformable);
                
                var space = _collisionActors[spaceId].CollisionType;
                Debug.Log(space);

                transformable.Transformable.DisableObject();
                arg2.gameObject.SetActive(false);

                _collisionActors.Add(new CollisionActors(arg1, transformable.Transformable, space));
                return;
            }
            //при столкновении с астероидом остылать евент спавнеру тот возрващает объект в пул и достает 2 меньших размера

            if (arg1.transform.CompareTag(Tags.Enemy) && arg2.CompareTag(Tags.Player))
            {
                var transformable = _collisionActors
                    .Select(col => col)
                    .First(t => t.Transformable.Prefab == arg2.gameObject);
                
                var spaceId = _collisionActors.IndexOf(transformable);
                var space = _collisionActors[spaceId].CollisionType;
                Debug.Log(space);
 
                transformable.Transformable.DisableObject();
                
                _collisionActors.Add(new CollisionActors(arg1, transformable.Transformable, space));
                arg1.gameObject.SetActive(false);
            }
        }
    }
}