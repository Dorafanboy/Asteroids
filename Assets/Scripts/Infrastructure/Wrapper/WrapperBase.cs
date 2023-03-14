using UnityEngine;

namespace Infrastructure.Wrapper
{
    public abstract class WrapperBase : IUpdateListener
    {
        private readonly IUpdatable _updatable;
        protected Camera Camera { get; }

        protected WrapperBase(IUpdatable updatable, Camera camera)
        {
            _updatable = updatable;
            Camera = camera;
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
    }
}