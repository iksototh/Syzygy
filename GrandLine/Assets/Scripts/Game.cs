using GrandLine.Core.Enums;
using GrandLine.World;
using GrandLine.Overlays;

namespace GrandLine
{
    internal static class Game
    {
        public static IMap WorldMap;
        public static IOverlay Overlay;

        public static GameStates GameState;

        public static void Pause() => GameState = GameStates.Paused;
        public static void Resume() => GameState = GameStates.Running;
        public static bool IsPaused = GameState == GameStates.Paused;
    }
}
