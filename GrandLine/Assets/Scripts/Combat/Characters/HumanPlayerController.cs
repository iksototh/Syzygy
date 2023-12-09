using GrandLine.Farming;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GrandLine.Combat.Characters
{
    public class HumanPlayerController : MonoBehaviour
    {
        private ShipMovementController _shipMovementController;
        private Camera _mainCamera;
        private void OnPathComplete()
        {
            Debug.Log("Complete");
            // SceneManager.LoadScene("Town");
        }

        private void OnPathFailed()
        {
            Debug.Log("Failed");
        }

        private void Awake()
        {
            _shipMovementController = GetComponent<ShipMovementController>();
            _shipMovementController.OnPathComplete = OnPathComplete;
            _shipMovementController.OnPathFailed = OnPathFailed;
            _mainCamera = Camera.main;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Trigger");
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("Colliusion");
        }

        // Update is called once per frame
        void Update()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame && FarmingManager.Instance.PlayerCanMove())
            {
                var end = Game.Instance.WorldMap.WorldToCell(_mainCamera.ScreenToWorldPoint(Input.mousePosition));
                _shipMovementController.TravelTo(end);
            }
        }
    }
}
