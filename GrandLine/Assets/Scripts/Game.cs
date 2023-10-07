using GrandLine.Core.Enums;
using GrandLine.World;
using GrandLine.Overlays;
using GrandLine.Systems.Savegame;

namespace GrandLine
{
    internal class Game
    {
        public static IMap WorldMap;
        public static IOverlay Overlay;

        public static GameStates GameState;

        public static void Pause() => GameState = GameStates.Paused;
        public static void Resume() => GameState = GameStates.Running;
        public static bool IsPaused = GameState == GameStates.Paused;

        public static SavegameManager SavegameManager;
    }
}
