using Cinemachine;
using GrandLine.Core.Enums;
using GrandLine.Overlays;
using GrandLine.World;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GrandLine
{
    public class Bootstrap : MonoBehaviour
    {
        public Grid WorldGrid;
        public Tilemap OverlayMap;
        public GameObject Player;
        public Tile OverlayGreenTile;
        public Tile OverlayRedTile;
        public Tile OverlayYellowTile;
        public Tile OverlayBlueTile;
        public Tile OverlayBlackTile;
        public Tile OverlayWhiteTile;

        void Awake()
        {
            var player = Instantiate(Player, new Vector3(-2.5f, 5.5f), Quaternion.identity);
            var playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
            var cinematicScript = playerCamera.GetComponent<CinemachineVirtualCamera>();
            cinematicScript.Follow = player.transform;
            cinematicScript.LookAt = player.transform;

            Game.WorldMap = new WorldMap(WorldGrid);

            var overlay = new Overlay(OverlayMap);
            overlay.SetTile(OverlayRedTile, TileOverlays.Red);
            overlay.SetTile(OverlayGreenTile, TileOverlays.Green);
            overlay.SetTile(OverlayBlackTile, TileOverlays.Black);
            overlay.SetTile(OverlayWhiteTile, TileOverlays.White);
            overlay.SetTile(OverlayBlueTile, TileOverlays.Blue);
            overlay.SetTile(OverlayYellowTile, TileOverlays.Yellow);
            Game.Overlay = overlay;
        }
    }
}
