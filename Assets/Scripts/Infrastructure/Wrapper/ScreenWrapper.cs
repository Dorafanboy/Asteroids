using Entities.Guns;
using UnityEngine;

namespace Infrastructure.Wrapper
{
    public class ScreenWrapper : WrapperBase 
    {
        private readonly ITransformable _transformable;
        
        public ScreenWrapper(IUpdatable updatable, Camera camera, ITransformable transformable) : base(updatable, camera)
        {
            _transformable = transformable;
        }

        public override void OnUpdated(float time)
        {
            var viewportPosition = GetViewportPoint(_transformable);
            var newPosition = _transformable.Prefab.transform.position;

            newPosition.x = GetWrapPosition(viewportPosition.x, newPosition.x);
            newPosition.y = GetWrapPosition(viewportPosition.y, newPosition.y);
        
            _transformable.InstallPosition(newPosition); 
        }
    }
}