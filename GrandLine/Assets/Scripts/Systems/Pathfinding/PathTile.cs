using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace GrandLine
{
    class PathTile
    {
        public PathTile(int score, int x, int y)
        {
            Score = score;
            X = x;
            Y = y;
        }

        public PathTile Parent;
        public int Score;
        public int X;
        public int Y;

        public Vector3Int ToVector3Int()
        {
            return new Vector3Int(X, Y, 0);
        }

        public Vector2 ToVector2()
        {
            return new Vector2(X, Y);
        }

        public override string ToString()
        {
            return $"{X}, {Y}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var tile = obj as PathTile;
            if (tile.X == X && tile.Y == Y) return true;

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Parent, Score, X, Y);
        }
    }
}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//namespace GrandLine
//{
//    public class Pathfinder
//    {
//        const int MAX_CALCULATED_PATHS = 10000;

//        Rigidbody2D _rigidbody2d;

//        Vector3 moveTowards;

//        private PathTile _target;

//        private List<PathTile> FinalPathList = new List<PathTile>();

//        public Action OnPathComplete;
//        public Action OnPathFailed;

//        public Pathfinder(Rigidbody2D rigidbody2d)
//        {
//            _rigidbody2d = rigidbody2d;
//        }

//        public void SetTarget(Vector3Int target, int maxMoves = -1)
//        {
//            ClearTarget();

//            var startCell = GetCell(_rigidbody2d.position);

//            var targetCoords = target;
//            _target = new PathTile(0, targetCoords.x, targetCoords.y);

//            Debug.Log($"Start {startCell}");
//            Debug.Log($"End {_target}");

//            var finalPath = CalculatePath(startCell, target);
//            FinalPathList = finalPath;
//            if (maxMoves != -1)
//            {
//                FinalPathList = finalPath.Take(maxMoves).ToList();
//            }

//            var moveTo = FinalPathList.First();
//            moveTowards = new Vector2(moveTo.X, moveTo.Y);
//        }

//        public void ClearTarget()
//        {
//            FinalPathList.Clear();
//        }

//        Vector3Int GetCell(Vector2 target)
//        {
//            return Game.WorldMap.WorldToCell(target);
//        }

//        int ManhattanScore(Vector3Int position, Vector3Int target)
//        {
//            return Math.Abs(position.x - target.x) + Math.Abs(position.y - target.y);
//        }

//        bool IsBlockedCell(int x, int y)
//        {
//            var colliderType = Game.WorldMap.CollisionMap.GetColliderType(new Vector3Int(x, y));
//            return colliderType != UnityEngine.Tilemaps.Tile.ColliderType.None;
//        }

//        bool IsBlockedCell(PathTile pathTile)
//        {
//            return IsBlockedCell(pathTile.X, pathTile.Y);
//        }

//        public List<PathTile> CalculatePath(Vector3Int startPostion, Vector3Int targetPosition)
//        {
//            var target = new PathTile(0, targetPosition.x, targetPosition.y);
//            if (IsBlockedCell(target) && !Game.WorldMap.IsTown(new Vector3Int(target.X, target.Y)))
//            {
//                // return null;
//            }
//            var start = new PathTile(ManhattanScore(startPostion, targetPosition), startPostion.x, startPostion.y);
//            var openList = new List<PathTile>() { start };
//            var finalPathList = new List<PathTile>();
//            var closedList = new List<PathTile>();

//            void AddPathPart(PathTile pathPart, PathTile parent)
//            {
//                if ((IsBlockedCell(pathPart) && !Game.WorldMap.IsTown(new Vector3Int(pathPart.X, pathPart.Y))) || openList.Contains(pathPart) || closedList.Contains(pathPart))
//                {
//                    return;
//                }

//                pathPart.Parent = parent;
//                pathPart.Score = ManhattanScore(new Vector3Int(pathPart.X, pathPart.Y), new Vector3Int(target.X, target.Y));
//                openList.Add(pathPart);
//            }

//            var calculatedPaths = 0;
//            while (calculatedPaths < MAX_CALCULATED_PATHS || openList.Count == 0)
//            {
//                calculatedPaths = calculatedPaths + 1;

//                var startingCell = openList.OrderBy(cell => cell.Score).FirstOrDefault();
//                if (startingCell == null) break;

//                closedList.Add(startingCell);
//                openList.Remove(startingCell);

//                if (startingCell.X == target.X && startingCell.Y == target.Y) break;

//                var north = new PathTile(-1, startingCell.X, startingCell.Y + 1);
//                var northEast = new PathTile(-1, startingCell.X + 1, startingCell.Y + 1);
//                var east = new PathTile(-1, startingCell.X + 1, startingCell.Y);
//                var southEast = new PathTile(-1, startingCell.X + 1, startingCell.Y - 1);
//                var south = new PathTile(-1, startingCell.X, startingCell.Y - 1);
//                var southWest = new PathTile(-1, startingCell.X - 1, startingCell.Y - 1);
//                var west = new PathTile(-1, startingCell.X - 1, startingCell.Y);
//                var northWest = new PathTile(-1, startingCell.X - 1, startingCell.Y + 1);
//                AddPathPart(north, startingCell);
//                AddPathPart(east, startingCell);
//                AddPathPart(south, startingCell);
//                AddPathPart(west, startingCell);

//                //AddPathPart(northEast, startingCell);
//                //AddPathPart(southEast, startingCell);
//                //AddPathPart(southWest, startingCell);
//                //AddPathPart(northWest, startingCell);
//            }

//            if (calculatedPaths >= 1000)
//            {
//                Debug.Log("Failed to find a path");

//                var lowestScore = closedList.OrderBy(cell => cell.Score).First().Score;
//                closedList = closedList.TakeWhile(cell => cell.Score > lowestScore).ToList();
//            }

//            var finalPath = closedList.Last();
//            void AddFinalPath(PathTile path)
//            {
//                if (path == null) return;
//                finalPathList.Add(path);

//                AddFinalPath(path.Parent);
//            }

//            AddFinalPath(finalPath);
//            finalPathList.Reverse();



//            return finalPathList;
//        }

//        public Vector2 GetTarget()
//        {
//            var cell = Game.WorldMap.WorldToCell(_rigidbody2d.position);

//            if (moveTowards != cell) return moveTowards;

//            var newPosition = FinalPathList.FirstOrDefault();
//            if (newPosition == null)
//            {
//                OnPathComplete();
//                return moveTowards;
//            };

//            FinalPathList.Remove(newPosition);
//            moveTowards = new Vector2(newPosition.X, newPosition.Y);

//            return moveTowards;
//        }

//        Vector2 direction = new Vector2(0, 0);

//        public Vector2 GetDirection()
//        {
//            Vector3 position = _rigidbody2d.position;
//            // N
//            if (direction.y == 1 && direction.x == 0)
//            {
//                position = new Vector3(position.x, position.y - 0.5f);
//            }

//            // NE
//            if (direction.y == 1 && direction.x == 1)
//            {
//                position = new Vector3(position.x - 0.5f, position.y - 0.5f);
//            }

//            // E
//            if (direction.y == 0 && direction.x == 1)
//            {
//                position = new Vector3(position.x - 0.5f, position.y);
//            }

//            // SE
//            if (direction.y == -1 && direction.x == 1)
//            {
//                position = new Vector3(position.x - 0.5f, position.y + 0.5f);
//            }

//            // S
//            if (direction.y == -1 && direction.x == 0)
//            {
//                position = new Vector3(position.x, position.y + 0.5f);
//            }

//            // SW
//            if (direction.y == -1 && direction.x == -1)
//            {
//                position = new Vector3(position.x + 0.5f, position.y + 0.5f);
//            }

//            // W
//            if (direction.y == 0 && direction.x == -1)
//            {
//                position = new Vector3(position.x + 0.5f, position.y);
//            }

//            // NW
//            if (direction.y == 1 && direction.x == -1)
//            {
//                position = new Vector3(position.x + 0.5f, position.y - 0.5f);
//            }


//            var cell = Game.WorldMap.WorldToCell(position);

//            if (moveTowards != cell) return direction;

//            var newPosition = FinalPathList.FirstOrDefault();
//            if (newPosition == null)
//            {
//                OnPathComplete();
//                direction = new Vector2(0, 0);
//                return direction;
//            };

//            FinalPathList.Remove(newPosition);
//            direction = new Vector2(newPosition.X - cell.x, newPosition.Y - cell.y);
//            moveTowards = new Vector2(newPosition.X, newPosition.Y);

//            return direction;
//        }

//        internal List<PathTile> GetFinalPath()
//        {
//            return FinalPathList;
//        }
//    }
//}