using System;
using UnityEngine;

namespace GrandLine.Systems.Savegame
{
    [Serializable]
    public class NavigationData
    {
        [SerializeField]
        public Vector3Int TravelTo;
        [SerializeField]
        public Vector3Int CurrentPosition;
    }
}