using Infrastructure;
using UnityEngine;

namespace Guns
{
    public class ProjectilePresenter : IUpdateListener
    {
        private readonly Projectile _projectile;
        private readonly ProjectileView _view;
        private readonly IUpdatable _updatable;

        public ProjectilePresenter(Projectile projectile, ProjectileView view, IUpdatable updatable)
        {
            _projectile = projectile;
            _view = view;
            _updatable = updatable;
            
            Enable();
        }

        public void Enable()
        {
            _updatable.Updated += OnUpdated;
        }

        public void Disable()
        {
            _updatable.Updated -= OnUpdated;
        }

        public void OnUpdated(float time)
        {
            _projectile.Prefab.transform.position += time * _projectile.Prefab.transform.up * 
                                                     _projectile.Deceleration;
        }
    }
}