using Entities.Guns;
using UnityEngine;

namespace Infrastructure.Wrapper
{
    public abstract class WrapperBase : IUpdateListener
    {
        private readonly IUpdatable _updatable;
        private readonly Camera _camera;

        protected WrapperBase(IUpdatable updatable, Camera camera)
        {
            _updatable = updatable;
            _camera = camera;
        }

        public virtual void Enable()
        {
            _updatable.Updated += OnUpdated;
        }

        public virtual void Disable()
        {
            _updatable.Updated -= OnUpdated;
        }

        public abstract void OnUpdated(float time);

        protected float GetWrapPosition(float viewportPosition, float newPosition)
        {
            return viewportPosition > 1 || viewportPosition < 0 ? -newPosition : newPosition;
        }

        protected bool IsNeedWrap(ITransformable transformable)
        {
            var viewportPosition = GetViewportPoint(transformable);
            
            return viewportPosition.x > 1 || viewportPosition.x < 0 || viewportPosition.y < 0 || viewportPosition.y > 1;
        }

        protected Vector3 GetViewportPoint(ITransformable transformable)
        {
            var position = transformable.Prefab.transform.position;
            return _camera.WorldToViewportPoint(position);
        }
    }
}