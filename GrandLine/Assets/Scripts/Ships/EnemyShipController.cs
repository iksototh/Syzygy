using GrandLine.World;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GrandLine
{
    public class EnemyShipController : MonoBehaviour
    {
        PathFinder _pathfinder;
        Rigidbody2D _rigidbody2d;

        public float BaseSpeed = 1;

        List<ITile> _towns;

        //private TileInfo _tileTarget;
        //private Vector3Int? _shipTarget;
        //private bool _dirtyTarget = false;
        private ShipMovementController _shipMovementController;

        private void Awake()
        {
            _shipMovementController = GetComponent<ShipMovementController>();
            _towns = Game.WorldMap.GetAllTowns();

        }

        // This should go into some form of AI manager
        Vector3Int GetNextTarget()
        {
            
            var townCount = _towns.Count;
            var index = new System.Random().Next(townCount);
            var town = _towns[index];
            
            return town.ToVector3Int();
        }

        //void GetNewWarTarget()
        //{
        //    SeeOtherShips(30f);
        //}


        void OnPathComplete()
        {
            Debug.Log("Target reached");
            _shipMovementController.TravelTo(GetNextTarget());
        }

        void OnPathFailed()
        {
            Debug.Log("Target failed");
            _shipMovementController.TravelTo(GetNextTarget());
        }

        void Start()
        {
            _rigidbody2d = GetComponent<Rigidbody2D>();
            _shipMovementController.OnPathComplete = OnPathComplete;
            _shipMovementController.OnPathFailed = OnPathFailed;
            _shipMovementController.TravelTo(GetNextTarget());
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            //var target = _pathfinder.GetTarget();
            //var cell = Game.WorldMap.WorldToCell(collision.transform.position);

            //if (target.x == cell.x && target.y == cell.y)
            //{
            //    Debug.Log("Collided with target");
            //    _pathfinder.OnPathComplete();
            //    return;
            //}

            Debug.Log("Collided with something");
        }

        // Update is called once per frame
        //void FixedUpdate()
        //{
        //    Move();
        //    // SeeOtherShips();
        //}

        // This can be improved, currently it is all seeing within distance
        //void SeeOtherShips(float distance = 5f)
        //{
        //    var shipLayerMask = LayerMask.GetMask("Ship");

        //    var hits = Physics2D.OverlapCircleAll(gameObject.transform.position, distance, shipLayerMask);

        //    if (hits.Any())
        //    {
        //        var target = Game.WorldMap.WorldToCell(hits.First().transform.position);
        //        if (_shipTarget != null && _shipTarget.Value.x == target.x && _shipTarget.Value.y == target.y) return;

        //        _shipTarget = target;
        //        _pathfinder.SetTarget(target);
        //    }
        //    else
        //    {
        //        if (_dirtyTarget)
        //            _pathfinder.SetTarget(_tileTarget.Coordinates);
        //        _shipTarget = null;
        //        _dirtyTarget = false;
        //    }
        //}

        //void Move()
        //{
        //    Vector2 position = _rigidbody2d.position;
        //    var realSpeed = GetMovementSpeed() * Time.deltaTime;
        //    var target = _pathfinder.GetTarget();
        //    _rigidbody2d.MovePosition(Vector2.MoveTowards(position, new Vector2(target.x + .5f, target.y + .5f), realSpeed));
        //}

        //float GetMovementSpeed()
        //{
        //    //var cell = Game.WorldMap.WorldToCell(_rigidbody2d.position);
        //    //var tile = Game.WorldMap.GetTile(cell);

        //    //if (tile.name == "ocean2") return BaseSpeed * 0.5f;

        //    return BaseSpeed;
        //}

        //void OnDrawGizmos()
        //{
        //    if (_pathfinder == null) return;

        //    var paths = _pathfinder.GetFinalPath();
        //    if (paths == null) return;

        //    foreach (var path in paths)
        //    {
        //        Gizmos.color = Color.red;
        //        Gizmos.DrawCube(new Vector3(path.X + 0.5f, path.Y + 0.5f), new Vector3(1, 1));
        //    }

        //    if (_towns != null)
        //    {
        //        foreach (var town in _towns)
        //        {
        //            Gizmos.color = Color.yellow;
        //            Gizmos.DrawCube(new Vector3(town.Coordinates.x + 0.5f, town.Coordinates.y + 0.5f), new Vector3(1, 1));
        //        }
        //    }

        //    // Gizmos.color = Color.green;
        //    // Gizmos.DrawCube(new Vector3(_tileTarget.Coordinates.x + 0.5f, _tileTarget.Coordinates.y + 0.5f), new Vector3(1, 1));
        //}
    }
}