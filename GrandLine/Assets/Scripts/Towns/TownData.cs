using GrandLine.Core.Enums;
using GrandLine.World;
using UnityEngine;

namespace GrandLine.Towns
{
    [CreateAssetMenu]
    public class TownData : ScriptableObject
    {
        public TownTypes TownType;
        public ITile InitiatorTile;
        public string EncounterId;
    }
}