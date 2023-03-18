using Entities.Guns;

namespace Infrastructure.Services.Clashes
{
    public readonly struct CollisionActors
    {
        public ITransformable Transformable { get; }
        public CollisionChecker CollisionChecker { get; }
        
        public CollisionActors(CollisionChecker collisionChecker, ITransformable transformable)
        {
            CollisionChecker = collisionChecker;
            Transformable = transformable;
        }
    }
}