using GrandLine.Core.Enums;
using UnityEngine;

namespace GrandLine.Overlays
{
    interface IOverlay
    {
        public void Clear();
        public void DrawGreenTile(Vector3Int position);
        public void DrawRedTile(Vector3Int position);
        public void DrawBlueTile(Vector3Int position);
        public void DrawWhiteTile(Vector3Int position);
        public void DrawYellowTile(Vector3Int position);
        public void DrawBlackTile(Vector3Int position);
    }
}
