using UnityEngine;
using UnityEngine.Tilemaps;

namespace GrandLine.Overlays
{
    class OverlayManager : MonoBehaviour
    {
        public Tilemap OverlayMap;
        public Tile OverlayGreenTile;
        public Tile OverlayRedTile;
        public Tile OverlayYellowTile;
        public Tile OverlayBlueTile;
        public Tile OverlayBlackTile;
        public Tile OverlayWhiteTile;

        public static OverlayManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void Clear()
        {
            OverlayMap.ClearAllTiles();
        }

        public void DrawGreenTile(Vector3Int position)
        {
            OverlayMap.SetTile(position, OverlayGreenTile);
        }

        public void DrawRedTile(Vector3Int position)
        {
            OverlayMap.SetTile(position, OverlayRedTile);
        }

        public void DrawWhiteTile(Vector3Int position)
        {
            OverlayMap.SetTile(position, OverlayWhiteTile);
        }

        public void DrawBlueTile(Vector3Int position)
        {
            OverlayMap.SetTile(position, OverlayBlueTile);
        }

        public void DrawYellowTile(Vector3Int position)
        {
            OverlayMap.SetTile(position, OverlayYellowTile);
        }

        public void DrawBlackTile(Vector3Int position)
        {
            OverlayMap.SetTile(position, OverlayBlackTile);
        }
    }
}
