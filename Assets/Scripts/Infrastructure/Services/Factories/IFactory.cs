using ShipContent;
using UnityEngine;

namespace Infrastructure.Services.Factories
{
    public interface IFactory : IService
    {
        Ship CreateShip();
    }
}