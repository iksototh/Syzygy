using GrandLine.Core.Enums;
using GrandLine.World;
using GrandLine.Overlays;
using GrandLine.Systems.Savegame;
using GrandLine.Managers;
using GrandLine.Data;

namespace GrandLine
{
    internal static class Game
    {
        public static IMap WorldMap;
        public static IOverlay Overlay;

        public static GameStates GameState;

        public static SavegameManager SavegameManager;
        public static GameManager GameManager;
        public static CombatManager CombatManager;

        public static SceneData SceneData;
    }
}
