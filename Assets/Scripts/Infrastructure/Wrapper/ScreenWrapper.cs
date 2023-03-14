using Entities.Ship;
using UnityEngine;

namespace Infrastructure.Wrapper
{
    public class ScreenWrapper : WrapperBase 
    {
        private readonly ShipView _shipView;
        
        public ScreenWrapper(IUpdatable updatable, Camera camera, ShipView shipView) : base(updatable, camera)
        {
            _shipView = shipView;
        }
        
        public override void OnUpdated(float time)
        {
            var position = _shipView.ShipPrefab.transform.position;
            var viewportPosition = Camera.WorldToViewportPoint(position);
            var newPosition = position;

            newPosition.x = GetWrapPosition(viewportPosition.x, newPosition.x);
            newPosition.y = GetWrapPosition(viewportPosition.y, newPosition.y);
        
            _shipView.InstallPosition(newPosition); // install position in view
        }
    }
}