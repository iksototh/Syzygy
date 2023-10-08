using GrandLine.Systems.Savegame;
using System;
using UnityEngine;

namespace GrandLine
{
    public class EnemyShipController : MonoBehaviour
    {
        public float BaseSpeed = 1;

        public Vector3Int InitialTarget;

        private ShipMovementController _shipMovementController;

        private void Awake()
        {
            _shipMovementController = GetComponent<ShipMovementController>();
        }

        void Start()
        {
            _shipMovementController.OnPathComplete = OnPathComplete;
            _shipMovementController.OnPathFailed = OnPathFailed;
            _shipMovementController.TravelTo(InitialTarget != null ? InitialTarget : GetNextTarget());

            Game.SavegameManager.AddSaveable(OnSave);
        }

        Vector3Int GetNextTarget()
        {
            var nextTarget = Game.WorldMap.GetRandomTown().ToVector3Int();
            return nextTarget;
        }

        void OnPathComplete()
        {
            // Debug.Log("Target reached");
            _shipMovementController.TravelTo(GetNextTarget());
        }

        void OnPathFailed()
        {
            // Debug.Log("Target failed");
            _shipMovementController.TravelTo(GetNextTarget());
        }

        

        private ShipState OnSave()
        {
            return new ShipState
            {
                Id = new Guid().ToString(),
                Type = Core.Enums.SaveableTypes.Enemy,
                State = _shipMovementController.GetNavigationData()
            };
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("Collided with something");
        }
    }
}