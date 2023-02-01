using ShipContent;
using UnityEngine;

namespace Infrastructure.Wrapper
{
    public class ScreenWrapper : IUpdateListener
    {
        private readonly IUpdatable _updatable;
        private readonly Ship _ship;
        private readonly Camera _camera;
    
        public ScreenWrapper(IUpdatable updatable, Ship ship)
        {
            _updatable = updatable;
            _ship = ship;
            _camera = Camera.main;
        }

        public void Enable()
        {
            _updatable.Updated += OnUpdated;
        }

        public void Disable()
        {
            _updatable.Updated -= OnUpdated;
        }

        public void OnUpdated(float time)
        {
            var position = _ship.Prefab.transform.position;
            var viewportPosition = _camera.WorldToViewportPoint(position);
            var newPosition = position;

            newPosition.x = GetWrapPosition(viewportPosition.x, newPosition.x);
            newPosition.y = GetWrapPosition(viewportPosition.y, newPosition.y);
        
            _ship.Prefab.transform.position = newPosition;
        }
        
        private float GetWrapPosition(float viewportPosition, float newPosition)
        {
            return viewportPosition > 1 || viewportPosition < 0 ? -newPosition : newPosition;
        }
    }
}