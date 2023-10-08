using GrandLine.Core.Enums;
using System;

namespace GrandLine.Systems.Savegame
{
    [Serializable]
    public class ShipState
    {
        public string Id;

        public SaveableTypes Type;

        public NavigationData State;
    }
}