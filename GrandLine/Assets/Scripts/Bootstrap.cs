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
        public GameObject Enemy;
        public Tile OverlayGreenTile;
        public Tile OverlayRedTile;
        public Tile OverlayYellowTile;
        public Tile OverlayBlueTile;
        public Tile OverlayBlackTile;
        public Tile OverlayWhiteTile;

        void Awake()
        {
            Game.WorldMap = new WorldMap(WorldGrid);

            // Spawn some enemies
            var towns = Game.WorldMap.GetAllTowns();
            var townCount = towns.Count;
            var index = new System.Random().Next(townCount);
            var town = towns[index];

            var player = Instantiate(Player, new Vector3(town.X + 0.5f, town.Y + 0.5f), Quaternion.identity);
            
            Debug.Log(town.ToVector3Int());
            Debug.Log(Game.WorldMap.WorldToCell(player.GetComponent<Rigidbody2D>().position));
            var playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
            var cinematicScript = playerCamera.GetComponent<CinemachineVirtualCamera>();
            cinematicScript.Follow = player.transform;
            cinematicScript.LookAt = player.transform;

            

            var overlay = new Overlay(OverlayMap);
            overlay.Clear();
            overlay.SetTile(OverlayRedTile, TileOverlays.Red);
            overlay.SetTile(OverlayGreenTile, TileOverlays.Green);
            overlay.SetTile(OverlayBlackTile, TileOverlays.Black);
            overlay.SetTile(OverlayWhiteTile, TileOverlays.White);
            overlay.SetTile(OverlayBlueTile, TileOverlays.Blue);
            overlay.SetTile(OverlayYellowTile, TileOverlays.Yellow);
            Game.Overlay = overlay;


            index = new System.Random().Next(townCount);
            town = towns[index];
            Instantiate(Enemy, new Vector3(town.X + 0.5f, town.Y + 0.5f), Quaternion.identity);
        }
    }
}
