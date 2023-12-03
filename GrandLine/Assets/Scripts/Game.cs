using GrandLine.Core.Enums;
using GrandLine.World;
using GrandLine.Systems.Savegame;
using GrandLine.Combat;
using GrandLine.Towns;
using GrandLine.Core;

namespace GrandLine
{
    internal static class Game
    {
        public static IMap WorldMap;
        public static GameStates GameState;

        public static SavegameManager SavegameManager;
        public static GameManager GameManager;
        public static CombatManager CombatManager;
        public static TownManager TownManager;

        public static GameData SceneData;
    }
}
