using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileTesting
{
    public class LevelManager
    {
        public Tile[,] Tiles { get; private set; }
        public Vector2 PlayerStartPosition { get; private set; }

        public Vector2 MobStartPosition { get; private set; }
        public Vector2 FishStartPosition { get; private set; }
        public Fish Fish { get; private set; }

        public LevelManager()
        {
            Tiles = GenerateMap("testbana.txt");

            Fish = new Fish(FishStartPosition);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile t in Tiles)
            {
                t.Draw(spriteBatch);
            }
            Fish.Draw(spriteBatch);
        }

        private Tile[,] GenerateMap(string map)
        {
            string[] mapData = File.ReadAllLines(map);

            int width = mapData[0].Length;
            int height = mapData.Length;
            Tile[,] tiles = new Tile[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Vector2 tilePosition = new Vector2(x * ConstantValues.TILE_WIDTH, y * ConstantValues.TILE_HEIGHT);
                    if (mapData[y][x] == '1')
                    {
                        PlayerStartPosition = tilePosition;
                    }
                    if (mapData[y][x] == '2')
                    {
                        MobStartPosition = tilePosition;
                        FishStartPosition = tilePosition;
                    }
                    tiles[x, y] = new Tile(tilePosition, mapData[y][x]);
                }
            }
            return tiles;
        }

        public Tile GetTileAtPosition(Vector2 vector)
        {
            return Tiles[(int)vector.X / ConstantValues.TILE_WIDTH, (int)vector.Y / ConstantValues.TILE_HEIGHT];
        }
    }
}
