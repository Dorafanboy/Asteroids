using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Infrastructure.Services.Containers
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
            var services = new List<TService>();

            var myClassList = _registeredTypes.Values
                .Where(x => x.GetType() == typeof(TService));

            Debug.Log("i;m in get services: " + typeof(TService));
            
            foreach (var item in myClassList)
            {
                Debug.Log(item);
            }

            return new List<TService>();
        }
    }
}
