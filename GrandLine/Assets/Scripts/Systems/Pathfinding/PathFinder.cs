using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace GrandLine
{
    public class PathFinder : MonoBehaviour
    {
        const int MAX_CALCULATED_PATHS = 10000;

        // public Vector2Int Target;

        public Action OnPathComplete;
        public Action OnPathFailed;

        private Rigidbody2D _rigidbody2D;

        void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        int ManhattanScore(Vector3Int position, Vector3Int target)
        {
            return Math.Abs(position.x - target.x) + Math.Abs(position.y - target.y);
        }

        Vector3Int GetCell(Vector2 target)
        {
            return Game.WorldMap.WorldToCell(target);
        }
        bool IsBlockedCell(int x, int y)
        {
            var colliderType = Game.WorldMap.CollisionMap.GetColliderType(new Vector3Int(x, y));
            return colliderType != UnityEngine.Tilemaps.Tile.ColliderType.None;
        }

        bool IsBlockedCell(PathTile pathTile)
        {
            return IsBlockedCell(pathTile.X, pathTile.Y);
        }

        internal List<PathTile> CalculatePath(Vector3Int targetPosition)
        {
            var startPosition = GetCell(_rigidbody2D.position);
            var target = new PathTile(0, targetPosition.x, targetPosition.y);
            if (IsBlockedCell(target))
            {
                return null;
            }

            var start = new PathTile(ManhattanScore(startPosition, targetPosition), startPosition.x, startPosition.y);
            var openList = new List<PathTile>() { start };
            var finalPathList = new List<PathTile>();
            var closedList = new List<PathTile>();

            void AddPathPart(PathTile pathPart, PathTile parent)
            {
                if ((IsBlockedCell(pathPart) && !Game.WorldMap.IsTown(new Vector3Int(pathPart.X, pathPart.Y))) || openList.Contains(pathPart) || closedList.Contains(pathPart))
                {
                    Debug.Log($"Blcoked {pathPart}");
                    return;
                }
                Game.Overlay.DrawBlueTile(pathPart.ToVector3Int());

                pathPart.Parent = parent;
                pathPart.Score = ManhattanScore(new Vector3Int(pathPart.X, pathPart.Y), new Vector3Int(target.X, target.Y));
                openList.Add(pathPart);
            }

            var calculatedPaths = 0;
            while (calculatedPaths < MAX_CALCULATED_PATHS || openList.Count == 0)
            {
                calculatedPaths = calculatedPaths + 1;

                var startingCell = openList.OrderBy(cell => cell.Score).FirstOrDefault();
                if (startingCell == null) break;

                closedList.Add(startingCell);
                openList.Remove(startingCell);

                if (startingCell.X == target.X && startingCell.Y == target.Y) break;

                var north = new PathTile(-1, startingCell.X, startingCell.Y + 1);
                var northEast = new PathTile(-1, startingCell.X + 1, startingCell.Y + 1);
                var east = new PathTile(-1, startingCell.X + 1, startingCell.Y);
                var southEast = new PathTile(-1, startingCell.X + 1, startingCell.Y - 1);
                var south = new PathTile(-1, startingCell.X, startingCell.Y - 1);
                var southWest = new PathTile(-1, startingCell.X - 1, startingCell.Y - 1);
                var west = new PathTile(-1, startingCell.X - 1, startingCell.Y);
                var northWest = new PathTile(-1, startingCell.X - 1, startingCell.Y + 1);
                AddPathPart(north, startingCell);
                AddPathPart(east, startingCell);
                AddPathPart(south, startingCell);
                AddPathPart(west, startingCell);

                //AddPathPart(northEast, startingCell);
                //AddPathPart(southEast, startingCell);
                //AddPathPart(southWest, startingCell);
                //AddPathPart(northWest, startingCell);
            }

            if (calculatedPaths >= 1000)
            {
                Debug.Log("Failed to find a path");

                var lowestScore = closedList.OrderBy(cell => cell.Score).First().Score;
                closedList = closedList.TakeWhile(cell => cell.Score > lowestScore).ToList();
            }

            var finalPath = closedList.Last();
            void AddFinalPath(PathTile path)
            {
                if (path == null) return;
                finalPathList.Add(path);

                AddFinalPath(path.Parent);
            }

            AddFinalPath(finalPath);
            finalPathList.Reverse();

            return finalPathList;
        }

        //internal async Task<List<PathTile>> CalculatePath(Vector3Int targetPosition)
        //{
        //    var startPosition = GetCell(_rigidbody2D.position);
        //    var target = new PathTile(0, targetPosition.x, targetPosition.y);
        //    if (IsBlockedCell(target))
        //    {
        //        return null;
        //    }

        //    var start = new PathTile(ManhattanScore(startPosition, targetPosition), startPosition.x, startPosition.y);
        //    var openList = new List<PathTile>() { start };
        //    var finalPathList = new List<PathTile>();
        //    var closedList = new List<PathTile>();

        //    void AddPathPart(PathTile pathPart, PathTile parent)
        //    {
        //        if ((IsBlockedCell(pathPart) && !Game.WorldMap.IsTown(new Vector3Int(pathPart.X, pathPart.Y))) || openList.Contains(pathPart) || closedList.Contains(pathPart))
        //        {

        //            Debug.Log($"Blcoked {pathPart} {closedList.Contains(pathPart)} {openList.Contains(pathPart)}");
        //            return;
        //        }
        //        Game.Overlay.DrawBlueTile(pathPart.ToVector3Int());

        //        pathPart.Parent = parent;
        //        pathPart.Score = ManhattanScore(new Vector3Int(pathPart.X, pathPart.Y), new Vector3Int(target.X, target.Y));
        //        openList.Add(pathPart);
        //    }

        //    var calculatedPaths = 0;
        //    while (calculatedPaths < MAX_CALCULATED_PATHS || openList.Count == 0)
        //    {
        //        // await Task.Delay(100);
                
        //        calculatedPaths = calculatedPaths + 1;

        //        var startingCell = openList.OrderBy(cell => cell.Score).FirstOrDefault();
        //        if (startingCell == null) break;

        //        closedList.Add(startingCell);
        //        openList.Remove(startingCell);

        //        if (startingCell.X == target.X && startingCell.Y == target.Y) break;

        //        var north = new PathTile(-1, startingCell.X, startingCell.Y + 1);
        //        var northEast = new PathTile(-1, startingCell.X + 1, startingCell.Y + 1);
        //        var east = new PathTile(-1, startingCell.X + 1, startingCell.Y);
        //        var southEast = new PathTile(-1, startingCell.X + 1, startingCell.Y - 1);
        //        var south = new PathTile(-1, startingCell.X, startingCell.Y - 1);
        //        var southWest = new PathTile(-1, startingCell.X - 1, startingCell.Y - 1);
        //        var west = new PathTile(-1, startingCell.X - 1, startingCell.Y);
        //        var northWest = new PathTile(-1, startingCell.X - 1, startingCell.Y + 1);
        //        AddPathPart(north, startingCell);
        //        AddPathPart(east, startingCell);
        //        AddPathPart(south, startingCell);
        //        AddPathPart(west, startingCell);
        //        foreach(var ce in closedList)
        //        {
        //            Game.Overlay.DrawBlueTile(ce.ToVector3Int());
        //        }
        //        //AddPathPart(northEast, startingCell);
        //        //AddPathPart(southEast, startingCell);
        //        //AddPathPart(southWest, startingCell);
        //        //AddPathPart(northWest, startingCell);
        //    }

        //    if (calculatedPaths >= 1000)
        //    {
        //        Debug.Log("Failed to find a path");

        //        var lowestScore = closedList.OrderBy(cell => cell.Score).First().Score;
        //        closedList = closedList.TakeWhile(cell => cell.Score > lowestScore).ToList();
        //    }

        //    var finalPath = closedList.Last();
        //    void AddFinalPath(PathTile path)
        //    {
        //        if (path == null) return;
        //        finalPathList.Add(path);

        //        AddFinalPath(path.Parent);
        //    }

        //    AddFinalPath(finalPath);
        //    finalPathList.Reverse();

        //    return finalPathList;
        //}
    }
}
