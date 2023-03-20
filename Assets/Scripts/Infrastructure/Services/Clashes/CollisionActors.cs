using Entities.Guns;

namespace Infrastructure.Services.Clashes
{
    public readonly struct CollisionActors
    {
        public ITransformable Transformable { get; }
        public CollisionChecker CollisionChecker { get; }
        public CollisionType CollisionType { get; }
        
        public CollisionActors(CollisionChecker collisionChecker, ITransformable transformable, CollisionType collisionType)
        {
            CollisionChecker = collisionChecker;
            Transformable = transformable;
            CollisionType = collisionType;
        }
    }
}

public enum CollisionType
{
    None = 0,
    Player = 1,
    Laser = BulletType.Laser,
    Projectile = BulletType.Projectile,
    Ufo = ShipType.Ufo,
    Asteroid = ShipType.Asteroid,
}

public enum ShipType
{
    Ufo = 0,
    Asteroid = 1
}