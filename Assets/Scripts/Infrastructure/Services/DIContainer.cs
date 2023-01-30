using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Infrastructure.Services
{
    public class DiContainer : IDiContainer
    {
        private readonly Dictionary<Type, IService> _registeredTypes;

        public DiContainer()
        {
            _registeredTypes = new Dictionary<Type, IService>();
        }

        public void Register<TService>(TService service) where TService : IService
        {
            if (_registeredTypes.ContainsKey(typeof(TService)))
            {
                return;
            }

            _registeredTypes.Add(typeof(TService), service);
        }

        public TService GetService<TService>() where TService : IService
        {
            return (TService)_registeredTypes[typeof(TService)];
        }

        public IEnumerable<TService> GetServices<TService>() where TService : IService
        {
            var count = _registeredTypes
                .Select(x => x.Value)
                .Where(x => x.GetType() == typeof(TService)) as IEnumerable<TService>;
            
            Debug.Log(count);
            return count;
        }
    }
}