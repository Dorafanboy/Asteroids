using Infrastructure.Wrapper;
using ShipContent;

namespace Infrastructure.Services.Factories
{
    public interface IFactory : IService
    {
        Ship CreateShip();
        ScreenWrapper CreateWrapper(Ship ship);
    }
}