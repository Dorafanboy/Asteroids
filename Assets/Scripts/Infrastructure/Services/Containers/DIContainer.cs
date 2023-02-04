using System;
using System.Collections.Generic;

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
    }
}