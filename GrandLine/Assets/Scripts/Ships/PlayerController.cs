using GrandLine.Systems.Savegame;
using GrandLine.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GrandLine.Ships
{
    public class PlayerController : MonoBehaviour
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

        private void Start()
        {
            Game.Instance.SavegameManager.AddSaveable(OnSave);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var trigger = collision.GetComponent<ITrigger>();
            if (trigger != null)
            {
                Debug.Log("Trigger something");
                trigger.OnTrigger();
            }            
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("Colliusion");
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (EventSystem.current.IsPointerOverGameObject()) return;
                
                var end = Game.Instance.WorldMap.WorldToCell(_mainCamera.ScreenToWorldPoint(Input.mousePosition));
                _shipMovementController.TravelTo(end);
            }
        }

        private void OnLoad(ShipState saveState)
        {
            Debug.Log("Load");
        }

        private ShipState OnSave()
        {
            return new ShipState
            {
                Id = "player",
                Type = Core.Enums.SaveableTypes.Player,
                State = _shipMovementController.GetNavigationData()
            };
        }
    }

}
