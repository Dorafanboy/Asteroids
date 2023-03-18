using System;
using Entities.Guns;
using UnityEngine;

namespace Entities.Ship
{
    public class ShipView : ITransformable
    {
        private readonly ShipModel _shipModel; //TODO: избавиться от модели отсюда, сделать install angle from presenter
        public GameObject Prefab { get; }

        public ShipView(ShipModel shipModel)
        {
            _shipModel = shipModel;
            Prefab = _shipModel.Prefab;
        }

        public void InstallPosition(Vector3 position)
        {
            _shipModel.Prefab.transform.position = position;
        }

        public void DisableObject()
        {
            Collided?.Invoke(this);
        }

        public event Action<ITransformable> Collided;

        public void InstallAngleRotation(float angle)
        {
            _shipModel.Prefab.transform.rotation = Quaternion.Lerp(_shipModel.Prefab.transform.rotation,
                Quaternion.Euler(0, 0, angle), _shipModel.RotationSpeed);
        }
    }
}