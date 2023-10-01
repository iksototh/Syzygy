using UnityEngine;

namespace GrandLine.Ships
{
    public class ShipController : MonoBehaviour
    {
        private ShipMovementController _shipMovementController;
        private Camera _mainCamera;

        private void Awake()
        {
            _shipMovementController = GetComponent<ShipMovementController>();
            _mainCamera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                Game.Overlay.Clear();
                var end = Game.WorldMap.WorldToCell(_mainCamera.ScreenToWorldPoint(Input.mousePosition));
                _shipMovementController.TravelTo(end);
            }
        }
    }
}
