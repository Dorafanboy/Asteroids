using Entities.Guns;
using Entities.Pool;
using UnityEngine;

namespace Entities.Enemy
{
    public abstract class EnemyEntityBase : IPoolProduct
    {
        public GameObject Prefab { get; }
        protected float Speed { get; }

        protected EnemyEntityBase(GameObject prefab, float speed)
        {
            Prefab = prefab;
            Speed = speed;
        }

        //TODO Сделать обработку коллизий
    }
}