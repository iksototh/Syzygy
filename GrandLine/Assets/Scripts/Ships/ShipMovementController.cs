using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GrandLine
{
    public class ShipMovementController : MonoBehaviour
    {
        public Action OnPathComplete;
        public Action OnPathFailed;

        private PathFinder _pathFinder;
        private Rigidbody2D _rigidbody2D;
        private Vector3Int _moveTowards;
        private List<PathTile> _path;
        private Vector2 _direction = new Vector2(0, 0);

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _pathFinder = new PathFinder();
        }
        public void TravelTo(Vector3Int end)
        {
            var newPath = _pathFinder.CalculatePath(_rigidbody2D.position, end);

            if (newPath == null)
            {
                if (_path != null && _path.Count > 0)
                {
                    _path = new List<PathTile>() { _path[0] };
                }
                OnPathFailed();
                return;
            }

            _path = newPath;
            var moveTowards = _path.FirstOrDefault();
            if (moveTowards != null)
            {
                _moveTowards = moveTowards.ToVector3Int();
            }
        }

        private void LateUpdate()
        {
            if (_path == null || _moveTowards == null)
            {
                _rigidbody2D.velocity = new Vector2(0, 0);
                return;
            }

            var target = GetDirection();
            if (target == null) return;

            _rigidbody2D.velocity = target.Value * 4f;
        }

        private Vector2? GetDirection()
        {
            var position = _rigidbody2D.position;
            var towards = _path.FirstOrDefault();
            if (towards == null)
            {
                return null;
            }

            var newPosition = GetDirection(_direction, position);

            var cell = Game.WorldMap.WorldToCell(newPosition);
            _direction = new Vector2(towards.X - cell.x, towards.Y - cell.y);


            if (towards.ToVector3Int() == cell)
            {
                _path.RemoveAt(0);
                if(_path.Count == 0)
                {
                    OnPathComplete();
                }
            }

            return _direction;
        }

        private static Vector3 GetDirection(Vector2 direction, Vector3 position)
        {
            // N
            if (direction.y == 1 && direction.x == 0)
            {
                return new Vector3(position.x, position.y - 0.5f);
            }

            // NE
            if (direction.y == 1 && direction.x == 1)
            {
                return new Vector3(position.x - 0.5f, position.y - 0.5f);
            }

            // E
            if (direction.y == 0 && direction.x == 1)
            {
                return new Vector3(position.x - 0.5f, position.y);
            }

            // SE
            if (direction.y == -1 && direction.x == 1)
            {
                return new Vector3(position.x - 0.5f, position.y + 0.5f);
            }

            // S
            if (direction.y == -1 && direction.x == 0)
            {
                return new Vector3(position.x, position.y + 0.5f);
            }

            // SW
            if (direction.y == -1 && direction.x == -1)
            {
                return new Vector3(position.x + 0.5f, position.y + 0.5f);
            }

            // W
            if (direction.y == 0 && direction.x == -1)
            {
                return new Vector3(position.x + 0.5f, position.y);
            }

            // NW
            if (direction.y == 1 && direction.x == -1)
            {
                return new Vector3(position.x + 0.5f, position.y - 0.5f);
            }

            return position;
        }
    }
}
