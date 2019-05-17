using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoyT.AStar;

namespace TheMaze
{
    static class Pathfind
    {
        private static Grid grid;

        //Metod som skapar grid-objektet som pathfindingen använder sig av. 
        //Behöver användas när man läser in kartan.
        public static void FillGridFromMap(Tile[,] map)
        {
            int width = map.GetLength(0);
            int height = map.GetLength(1);

            grid = new Grid(width, height, 1.0f);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (map[x, y].IsWall || map[x,y].IsEntrance)
                    {
                        grid.BlockCell(new Position(x, y));
                    }
                }
            }
        }

        //Metod som returnerar "the path" i en lista med positionerna för tiles man ska vandra på. 
        //Används av de monster som ska ha pathfinding. 
        public static List<Vector2> CreatePath(Vector2 startPos, Vector2 endPos)
        {
            Position start = VectorToPosition(startPos);
            Position end = VectorToPosition(endPos);
            List<Vector2> pathPos = new List<Vector2>();

            //if (CheckGridPos(start) && CheckGridPos(end))
            //{
            //    Console.WriteLine("###### Returning empty path.");
            //    return pathPos;
            //}

            Position[] path = grid.GetPath(start, end, MovementPatterns.LateralOnly);

            for (int i = 0; i < path.Length; i++)
            {
                pathPos.Add(PositionToVector(path[i]));
            }

            return pathPos;
        }

        //Converterar en Vektors position till ett Positions-objekt som pathfindingen använder sig av.
        private static Position VectorToPosition(Vector2 position)
        {
            return new Position((int)(position.X / ConstantValues.tileWidth), (int)(position.Y / ConstantValues.tileHeight));
        }

        //Converterar från ett Positions-objekt till en Vektors position som spelet använder sig av. 
        private static Vector2 PositionToVector(Position position)
        {
            return new Vector2(position.X * ConstantValues.tileWidth, position.Y * ConstantValues.tileHeight);
        }

        //Skapar en vektor med riktnigen mellan currentPos och nextPos för att kunna använda rörelse koden vi har.
        public static Vector2 SetDirectionFromNextPosition(Vector2 currentPos, Vector2 nextPos)
        {
            int x = 0;
            int y = 0;

            if (currentPos.X < nextPos.X)
            {
                x = 1;
            }
            if (currentPos.X > nextPos.X)
            {
                x = -1;
            }
            if (currentPos.Y < nextPos.Y)
            {
                y = 1;
            }
            if (currentPos.Y > nextPos.Y)
            {
                y = -1;
            }

            return new Vector2(x, y);
        }

        private static bool CheckGridPos(Position p)
        {
            //check if position is within grid limits
            if (p.X >= grid.DimX || p.X < 0 || p.Y >= grid.DimY || p.Y < 0)
            {
                Console.WriteLine("###### Grid Position X: " + p.X + " Y: " + p.Y + " is out of range!");
                return false;
            }
            return true;
        }
    }
}
