using GrandLine.Core.Enums;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GrandLine.Overlays
{
    class Overlay : IOverlay
    {
        private Tilemap _overlayTilemap;
        private Tile _greenTile;
        private Tile _redTile;
        private Tile _blackTile;
        private Tile _yellowTile;
        private Tile _whiteTile;
        private Tile _blueTile;

        public Overlay(Tilemap overlayTilemap)
        {
            _overlayTilemap = overlayTilemap;
        }

        public void SetTile(Tile tile, TileOverlays tileType)
        {
            switch(tileType)
            {
                case TileOverlays.Green:
                    _greenTile = tile;
                    break;
                case TileOverlays.Red:
                    _redTile = tile;
                    break;
                case TileOverlays.Black:
                    _blackTile = tile;
                    break;
                case TileOverlays.Yellow:
                    _yellowTile = tile;
                    break;            
                case TileOverlays.White:
                    _whiteTile = tile;
                    break;
                case TileOverlays.Blue:
                    _blueTile = tile;
                    break;
            }
        }

        public void Clear()
        {
            _overlayTilemap.ClearAllTiles();
        }

        public void DrawGreenTile(Vector3Int position)
        {
            _overlayTilemap.SetTile(position, _greenTile);
        }

        public void DrawRedTile(Vector3Int position)
        {
            _overlayTilemap.SetTile(position, _redTile);
        }

        public void DrawWhiteTile(Vector3Int position)
        {
            _overlayTilemap.SetTile(position, _whiteTile);
        }

        public void DrawBlueTile(Vector3Int position)
        {
            _overlayTilemap.SetTile(position, _blueTile);
        }

        public void DrawYellowTile(Vector3Int position)
        {
            _overlayTilemap.SetTile(position, _yellowTile);
        }

        public void DrawBlackTile(Vector3Int position)
        {
            _overlayTilemap.SetTile(position, _blackTile);
        }
    }
}
