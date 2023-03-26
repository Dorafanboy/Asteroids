using System;
using Entities.Guns;
using Infrastructure.Services.Clashes;
using UnityEngine;

namespace Entities.Ship
{
    public class ShipView : ITransformable
    {
        private readonly ShipModel _shipModel; //TODO: избавиться от модели отсюда, сделать install angle from presenter
        public GameObject Prefab { get; }
        public CollisionType CollisionType { get; }

        public ShipView(ShipModel shipModel)
        {
            _shipModel = shipModel;
            CollisionType = _shipModel.CollisionType;
            Prefab = _shipModel.Prefab;
        }

        public void InstallPosition(Vector3 position)
        {
            _shipModel.Prefab.transform.position = position;
        }

        public void DisableObject()
        {
            Collided?.Invoke(this);
            Prefab.SetActive(false);
        }

        public event Action<ITransformable> Collided;


        public void InstallAngleRotation(float angle)
        {
            _shipModel.Prefab.transform.rotation = Quaternion.Lerp(_shipModel.Prefab.transform.rotation,
                Quaternion.Euler(0, 0, angle), _shipModel.RotationSpeed);
        }
    }
}