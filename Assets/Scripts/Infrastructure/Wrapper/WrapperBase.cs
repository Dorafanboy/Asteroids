using UnityEngine;

namespace Infrastructure.Wrapper
{
    public abstract class WrapperBase
    {
        protected IUpdatable Updatable { get; }
        protected Camera Camera { get; }

        public WrapperBase(IUpdatable updatable, Camera camera)
        {
            Updatable = updatable;
            Camera = camera;
        }
    }
}