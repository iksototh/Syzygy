using GrandLine.Core.Enums;
using GrandLine.Systems.World;
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
        private Tilemap _collisionTilemap;


        public WorldMap(Grid worldGrid)
        {
            _worldGrid = worldGrid;
            var tileMaps = _worldGrid.GetComponentsInChildren<Tilemap>();
            _collisionTilemap = tileMaps.FirstOrDefault(tileMap => tileMap.tag == Tags.Collision.ToString());
            
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

        public Tilemap CollisionMap { get { return _collisionTilemap; } }

        private void AddTowns()
        {
            var tileMaps = _worldGrid.GetComponentsInChildren<Tilemap>();
            var towns = new List<ITile>();

            var townTileMap = tileMaps.FirstOrDefault(tileMap => tileMap.tag == Tags.Town.ToString());
            
            if(townTileMap != null)
            {
                foreach (var townPosition in townTileMap.cellBounds.allPositionsWithin)
                {
                    var tile = townTileMap.GetTile(townPosition);
                    if (tile == null) continue;

                    towns.Add(new TownTile() { X = townPosition.x, Y = townPosition.y });
                }
            }

            _towns = towns;
        }

        public ITile GetRandomTown()
        {
            var townCount = _towns.Count;
            var index = new System.Random().Next(townCount);
            var town = _towns[index];
            return town;
        }
    }
}
