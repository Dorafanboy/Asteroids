using Infrastructure;
using UnityEngine;

namespace Guns
{
    public class ProjectilePresenter : IUpdateListener
    {
        private readonly Bullet _bullet;
        private readonly ProjectileView _view;
        private readonly IUpdatable _updatable;

        public ProjectilePresenter(Bullet bullet, ProjectileView view, IUpdatable updatable)
        {
            _bullet = bullet;
            _view = view;
            _updatable = updatable;
            
            //Enable();
        }

        // public void Enable()
        // {
        //     _updatable.Updated += OnUpdated;
        // }
        //
        // public void Disable()
        // {
        //     _updatable.Updated -= OnUpdated;
        // }
        //
        // public void OnUpdated(float time)
        // {
        //     _projectile.Prefab.transform.position += time * _projectile.Prefab.transform.up * 
        //                                              _projectile.Deceleration;
        // }
        public void Enable()
        {
            
        }

        public void Disable()
        {
            
        }

        public void OnUpdated(float time)
        {
            
        }
    }
}