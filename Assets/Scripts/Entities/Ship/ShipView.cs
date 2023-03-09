using UnityEngine;

namespace Entities.Ship
{
    public class ShipView
    {
        private readonly ShipModel _shipModel;

        public ShipView(ShipModel shipModel)
        {
            _shipModel = shipModel;
        }

        public void InstallPosition(Vector3 position)
        {
            _shipModel.Prefab.transform.position = position;
        }

        public void InstallAngleRotation(float angle)
        {
            _shipModel.Prefab.transform.rotation = Quaternion.Lerp(_shipModel.Prefab.transform.rotation,
                Quaternion.Euler(0, 0, angle), _shipModel.RotationSpeed);
        }
    }
}