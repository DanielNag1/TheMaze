using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    public class LevelManager
    {
        public Tile[,] Tiles { get; private set; }
        public Vector2 StartPositionPlayer { get; private set; }
        public Vector2 StartPositionSafeRoom { get; private set; }
        public Vector2 ImbakuStartPosition { get; private set; }
        public Vector2 GlitchMonsterStartPosition { get; private set; }
        public Vector2 ArmMonsterStartPosition { get; private set; }
        public List<WallMonster> wallMonsters;
        public WallMonster wallMonster;

        public List<Collectible> collectibles;
        public Collectible collectible;

        public LevelManager()
        {
        }
        public void ReadLiveMap()
        {
            Tiles = GenerateMap("testbana.txt");
            Pathfind.FillGridFromMap(Tiles);
        }
        public void ReadDeathMap()
        {
            Tiles = GenerateMap("deathbana.txt");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile t in Tiles)
            {
                t.Draw(spriteBatch);
            }
        }

        public Tile GetTileAtPosition(Vector2 vector)
        {
            return Tiles[(int)vector.X / ConstantValues.tileWidth, (int)vector.Y / ConstantValues.tileHeight];
        }

        private Tile[,] GenerateMap(string map)
        {
            string[] mapData = File.ReadAllLines(map);
            collectibles = new List<Collectible>();
            wallMonsters = new List<WallMonster>();
            int width = mapData[0].Length;
            int height = mapData.Length;
            Tile[,] tiles = new Tile[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Vector2 tilePosition = new Vector2(x * ConstantValues.tileWidth, y * ConstantValues.tileHeight);
                    if (mapData[y][x] == '0')
                    {
                        StartPositionSafeRoom = tilePosition;
                    }
                    if (mapData[y][x] == '1')
                    {
                        StartPositionPlayer = new Vector2(tilePosition.X, tilePosition.Y - ConstantValues.tileHeight);
                    }
                    if (mapData[y][x] == '2')
                    {
                        ImbakuStartPosition = tilePosition;
                    }
                    if (mapData[y][x] == '3')
                    {
                        collectible = new Collectible(TextureManager.CollectibleTex, tilePosition);
                        collectibles.Add(collectible);
                    }
                    if (mapData[y][x] == '4')
                    {
                        wallMonster = new WallMonster(TextureManager.MonsterTex, tilePosition);
                        wallMonsters.Add(wallMonster);
                    }
                    if (mapData[y][x] == '5')
                    {
                        GlitchMonsterStartPosition = tilePosition;
                    }
                    if (mapData[y][x] == '6')
                    {
                        ArmMonsterStartPosition = tilePosition;
                    }
                    tiles[x, y] = new Tile(tilePosition, mapData[y][x]);
                }
            }
            return tiles;
        }
    }
}