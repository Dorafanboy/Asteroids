using Entities.Guns;
using Entities.Pool;

namespace Infrastructure.Spawners
{
    public class BulletSpawner : ISpawner
    {
        private readonly BulletObjectPool<Bullet> _pool;

        public BulletSpawner(BulletObjectPool<Bullet> pool)
        {
            _pool = pool;
        }

        public void Spawn()
        {
        }
    }
}