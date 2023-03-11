using System.Collections.Generic;

namespace Infrastructure.Services.Containers
{
    public interface IDiContainer
    {
        void Register<TService>(TService service) where TService : IService;
        TService GetService<TService>() where TService : IService;
        IEnumerable<TService> GetServices<TService>() where TService : IService;
    }
}