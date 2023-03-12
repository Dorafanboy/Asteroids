using Entities.Guns;
using Entities.Pool;
using Infrastructure.Services.Factories;

namespace Infrastructure.Spawners
{
    public class BulletSpawner : ISpawner
    {
        private readonly ObjectPool<Bullet> _pool;
        private readonly IFactory _factory;

        public BulletSpawner(ObjectPool<Bullet> pool, IFactory factory)
        {
            _pool = pool;
            _factory = factory;
        }

        public void Spawn()
        {
        }
    }
}