using GrandLine.World;
using UnityEngine;

namespace GrandLine.Systems.World
{
    public class BaseTile : ITile
    {
        public int X { get; set; }

        public int Y { get; set; }

        public virtual Vector3Int ToVector3Int()
        {
            return new Vector3Int(X, Y, 0);
        }
    }
}