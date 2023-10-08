using Cinemachine;
using GrandLine.Core.Enums;
using GrandLine.Overlays;
using GrandLine.Systems.Extensions;
using GrandLine.Systems.Savegame;
using GrandLine.World;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GrandLine
{
    public class GameManager : MonoBehaviour
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

        private GameObject _player;
        private List<GameObject> _ships;

        void Awake()
        {
            Game.WorldMap = new WorldMap(WorldGrid);
            Game.SavegameManager = new SavegameManager();
            Game.GameManager = this;

            AddOverlay();
            CreateGame();
        }

        private void CreateGame()
        {
            CreateWorld();
            SpawnPlayer();
            SpawnEnemies(5);
        }

        public void LoadGame(GameState gameState)
        {
            ClearGame();
            CreateWorld();
            
            foreach (var ship in gameState.ships)
            {
                if(ship.Type == SaveableTypes.Player)
                {
                    var tile = ship.State.CurrentPosition.ToBaseTile();
                    SpawnPlayer(tile);
                }
                if(ship.Type == SaveableTypes.Enemy)
                {
                    var enemy = SpawnEnemy(ship.State.CurrentPosition.ToBaseTile());
                    enemy.InitialTarget = ship.State.TravelTo;
                }
            }
        }

        private void ClearGame() 
        {
            Game.SavegameManager.ResetSaveableFuncs();
            Destroy(_player);
            foreach(var ship in _ships)
            {
                Destroy(ship);
            }
            _player = null;
            _ships = new List<GameObject>();
        }

        private void SpawnPlayer()
        {
            var spawnPoint = Game.WorldMap.GetRandomTown();
            SpawnPlayer(spawnPoint);
        }

        private void SpawnPlayer(ITile spawnPoint)
        {
            _player = Instantiate(Player, new Vector3(spawnPoint.X + 0.5f, spawnPoint.Y + 0.5f), Quaternion.identity);
            var playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
            var cinematicScript = playerCamera.GetComponent<CinemachineVirtualCamera>();
            cinematicScript.Follow = _player.transform;
            cinematicScript.LookAt = _player.transform;
        }

        private void SpawnEnemies(int count = 1)
        {
            var spawnPoints = new List<ITile>();
            for (var i = 0; i < count; i++)
            {
                var town = Game.WorldMap.GetRandomTown();
                spawnPoints.Add(town);
            }

            SpawnEnemies(spawnPoints);
        }

        private void SpawnEnemies(List<ITile> spawnPoints)
        {
            _ships = new List<GameObject>();

            for (var i = 0; i < spawnPoints.Count; i++)
            {
                var town = spawnPoints[i];
                SpawnEnemy(town);
            }
        }

        private EnemyShipController SpawnEnemy(ITile spawnPoint)
        {
            var ship = Instantiate(Enemy, new Vector3(spawnPoint.X + 0.5f, spawnPoint.Y + 0.5f), Quaternion.identity);
            _ships.Add(ship);
            var enemyScript = ship.GetComponent<EnemyShipController>();
            return enemyScript;
        }

        private void CreateWorld() { }

        private void CreateWorld(GameState worldState) { }

        private void AddOverlay()
        {
            var overlay = new Overlay(OverlayMap);
            overlay.Clear();
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
