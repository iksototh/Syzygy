using UnityEngine;

namespace GrandLine.World
{
    class TownTile : ITile
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Vector3Int ToVector3Int()
        {
            return new Vector3Int(X, Y, 0);
        }
    }
}
