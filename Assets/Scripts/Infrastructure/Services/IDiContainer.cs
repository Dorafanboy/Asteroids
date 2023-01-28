namespace Infrastructure.Services
{
    public interface IDiContainer
    {
        void Register<TService>(TService service) where TService : IService;
        TService GetService<TService>() where TService : IService;
    }
}