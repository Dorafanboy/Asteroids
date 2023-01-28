using UnityEngine;

namespace ShipContent
{
    public class ShipPresenter
    {
        private readonly Ship _ship;
        private readonly ShipView _view;

        public ShipPresenter(Ship ship, ShipView view)
        {
            _ship = ship;
            _view = view;
            _view.ShipRotated += OnShipRotated;
        }

        private void OnShipMoved(Vector3 position)
        {
            
        }
        
        private void OnShipRotated(float angle)
        {
            
        }
    }
}