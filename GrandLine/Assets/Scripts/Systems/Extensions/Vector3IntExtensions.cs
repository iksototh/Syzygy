using GrandLine.Systems.World;
using UnityEngine;

namespace GrandLine.Systems.Extensions
{
    public static class Vector3IntExtensions
    {
        public static BaseTile ToBaseTile(this Vector3Int vector3Int)
        {
            return new BaseTile()
            {
                X = vector3Int.x,
                Y = vector3Int.y,
            };
        }   
    }
}