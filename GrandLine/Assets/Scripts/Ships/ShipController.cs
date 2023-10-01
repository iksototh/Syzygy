using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace GrandLine.Ships
{
    public class ShipController : MonoBehaviour
    {
        private PathFinder _pathFinder;
        private Camera _mainCamera;
        private Rigidbody2D _rigidbody2D;
        private Vector3Int _moveTowards;
        private List<PathTile> _path;

        private void Awake()
        {
            _pathFinder = GetComponent<PathFinder>();
            _mainCamera = Camera.main;
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                Game.Overlay.Clear();
                var end = Game.WorldMap.WorldToCell(_mainCamera.ScreenToWorldPoint(Input.mousePosition));
                GetPath(end);
                
                
                //Debug.Log(_path.FirstOrDefault());
                //Debug.Log(Game.WorldMap.WorldToCell(_rigidbody2D.position));
                //_pathfinder.SetTarget(end, MaxMovementInWar);
                //_pathfinder.SetTarget(end);
                //paths = _pathfinder.CalculatePath(start, end);
                //moveTowards = end; // _pathfinder.GetTarget();
                // moving = true;
            }
        }

        void GetPath(Vector3Int end)
        {
            _path = _pathFinder.CalculatePath(end);
            Debug.Log($"Path {_path == null}");
            if (_path == null) return;

            var moveTowards = _path.FirstOrDefault();
            if (moveTowards != null)
            {
                _moveTowards = moveTowards.ToVector3Int();
            }
        }

        public void OnDrawGizmos()
        {
            if (_path == null) { return; }
            
            // Game.Overlay.Clear();

            foreach (var path in _path) 
            {
                Game.Overlay.DrawGreenTile(path.ToVector3Int());
            }
        }

        private void LateUpdate()
        {
            if(_path == null || _moveTowards == null) 
            {
                _rigidbody2D.velocity = new Vector2(0, 0);
                return; 
            }

            var target = GetDirection();
            if (target == null) return;

            _rigidbody2D.velocity = target.Value * 2f;
        }

        Vector2 direction = new Vector2(0, 0);

        public Vector2? GetDirection()
        {
            var position = _rigidbody2D.position;
            var towards = _path.FirstOrDefault();
            if(towards == null)
            {
                return null;
            }

            Game.Overlay.DrawRedTile(towards.ToVector3Int());

            var newPosition = GetDirection(direction, position);

            var cell = Game.WorldMap.WorldToCell(newPosition);
            direction = new Vector2(towards.X - cell.x, towards.Y - cell.y);

            Game.Overlay.DrawYellowTile(cell);
            
            if (towards.ToVector3Int() == cell)
            {
                _path.RemoveAt(0);
            }

            return direction;
        }

        static Vector3 GetDirection(Vector2 direction, Vector3 position)
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
