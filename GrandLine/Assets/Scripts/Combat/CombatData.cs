using GrandLine.Core.Enums;
using GrandLine.World;
using UnityEngine;

namespace GrandLine.Combat
{
    [CreateAssetMenu]
    public class CombatData : ScriptableObject
    {
        public CombatTypes CombatType;
        public ITile InitiatorTile;
        public string EncounterId;
    }
}