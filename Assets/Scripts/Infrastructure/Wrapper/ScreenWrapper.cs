using Entities.Ship;
using UnityEngine;

namespace Infrastructure.Wrapper
{
    public class ScreenWrapper : IUpdateListener 
    {
        private readonly IUpdatable _updatable;
        private readonly ShipModel _shipModel;
        private readonly Camera _camera;
    
        public ScreenWrapper(IUpdatable updatable, ShipModel shipModel, Camera camera)
        {
            _updatable = updatable;
            _shipModel = shipModel;
            _camera = camera; 
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
            var position = _shipModel.Prefab.transform.position;
            var viewportPosition = _camera.WorldToViewportPoint(position);
            var newPosition = position;

            newPosition.x = GetWrapPosition(viewportPosition.x, newPosition.x);
            newPosition.y = GetWrapPosition(viewportPosition.y, newPosition.y);
        
            _shipModel.Prefab.transform.position = newPosition;
        }
        
        private float GetWrapPosition(float viewportPosition, float newPosition)
        {
            return viewportPosition > 1 || viewportPosition < 0 ? -newPosition : newPosition;
        }
    }
}