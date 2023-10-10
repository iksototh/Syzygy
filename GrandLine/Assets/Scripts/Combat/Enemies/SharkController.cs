using System.Collections.Generic;
using UnityEngine;

namespace GrandLine.Combat.Enemies
{
    public class SharkController : MonoBehaviour
    {
        public float BaseSpeed = 1;

        public Vector3Int InitialTarget;

        private ShipMovementController _shipMovementController;
        private List<Vector3Int> _points = new List<Vector3Int>
            {
                new Vector3Int(5, 9),
                new Vector3Int(-6, 9),
                new Vector3Int(-6, -7),
                new Vector3Int(5, -7)
            };
        private int _currentPoint = -1;

        private void Awake()
        {
            _shipMovementController = GetComponent<ShipMovementController>();
            _shipMovementController.OnPathComplete = OnPathComplete;
            _shipMovementController.OnPathFailed = OnPathFailed;
        }

        void Start()
        {   
            _shipMovementController.TravelTo(InitialTarget != null ? InitialTarget : GetNextTarget());
        }

        Vector3Int GetNextTarget()
        {
            _currentPoint++;
            if (_currentPoint >= _points.Count) 
            {
                _currentPoint = 0;
            }
            Debug.Log($"{_points.Count} - {_currentPoint}");
            return _points[_currentPoint];
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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("Collided with something");
        }
    }
}
