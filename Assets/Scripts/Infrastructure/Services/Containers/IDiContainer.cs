namespace Infrastructure.Services.Containers
{
    public interface IDiContainer
    {
        void Register<TService>(TService service) where TService : IService;
        TService GetService<TService>() where TService : IService;
    }
}