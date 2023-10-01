using UnityEngine;
using UnityEngine.Tilemaps;

namespace GrandLine.World
{
    interface IMap
    {
        public Tilemap CollisionMap { get; }
        public Vector3Int WorldToCell(Vector3 worldPosition);

        public bool IsTown(Vector3 worldPosition);
        public bool IsTown(Vector3Int cell);
    }
}
