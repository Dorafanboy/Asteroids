using UnityEngine;

namespace ShipContent
{
    public class ShipView
    {
        private readonly Ship _ship;

        public ShipView(Ship ship)
        {
            _ship = ship;
        }

        public void InstallPosition(Vector3 position)
        {
            _ship.Prefab.transform.position += position;
        }

        public void InstallAngleRotation(float angle)
        {
            _ship.Prefab.transform.rotation = Quaternion.Lerp(_ship.Prefab.transform.rotation,
                Quaternion.Euler(0, 0, angle), _ship.RotationSpeed);
        }
    }
}