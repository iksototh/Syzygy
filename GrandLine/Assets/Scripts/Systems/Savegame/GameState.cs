using GrandLine.Systems.Savegame;
using System;
using System.Collections.Generic;

namespace GrandLine
{
    [Serializable]
    public class GameState
    {
        public List<ShipState> ships;
    }
}
