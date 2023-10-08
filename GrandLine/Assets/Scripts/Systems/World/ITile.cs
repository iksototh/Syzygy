using UnityEngine;

namespace GrandLine.World
{
    public interface ITile
    {
        int X { get; }
        int Y { get; }

        Vector3Int ToVector3Int();
    }
}
