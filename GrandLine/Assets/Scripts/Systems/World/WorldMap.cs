using GrandLine.Core.Enums;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GrandLine.World
{
    class WorldMap : IMap
    {
        private Grid _worldGrid;
        private List<ITile> _towns;

        public WorldMap(Grid worldGrid)
        {
            _worldGrid = worldGrid;
            AddTowns();
        }

        public Vector3Int WorldToCell(Vector3 worldPosition)
        {
            return _worldGrid.WorldToCell(worldPosition);
        }

        public bool IsTown(Vector3 worldPosition)
        {
            var cell = _worldGrid.WorldToCell(worldPosition);
            return IsTown(cell);
        }

        public bool IsTown(Vector3Int cell)
        {
            return _towns.Any(town => town.X == cell.x && town.Y == cell.y);
        }

        public List<ITile> GetAllTowns()
        {
            return _towns;
        }

        public Tilemap CollisionMap { get { return GetCollisionTilemap(); } }

        private Tilemap GetCollisionTilemap()
        {
            var tileMaps = _worldGrid.GetComponentsInChildren<Tilemap>();
            var collisionTilemap = tileMaps.FirstOrDefault(tileMap => tileMap.tag == Tags.Collision.ToString());

            return collisionTilemap;
        }

        private void AddTowns()
        {
            var tileMaps = _worldGrid.GetComponentsInChildren<Tilemap>();
            var towns = new List<ITile>();

            var townTileMap = tileMaps.FirstOrDefault(tileMap => tileMap.tag == Tags.Town.ToString());
            foreach (var townPosition in townTileMap.cellBounds.allPositionsWithin)
            {
                var tile = townTileMap.GetTile(townPosition);
                if (tile == null) continue;

                towns.Add(new TownTile() { X = townPosition.x, Y = townPosition.y });
            }

            _towns = towns;
        }
    }
}
