using Entities.Guns;
using Entities.Pool;

namespace Infrastructure.Spawners
{
    public class BulletSpawner : ISpawner
    {
        private readonly ObjectPool<Bullet> _pool;

        public BulletSpawner(ObjectPool<Bullet> pool)
        {
            _pool = pool;
        }

        public void Spawn()
        {
        }
    }
}