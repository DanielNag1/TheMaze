﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze
{
    public class TileManager
    {
        public Tile[,] Tiles { get; private set; }
        public Vector2 StartPositionPlayer { get; private set; }
        public Vector2 StartPositionMonster { get; private set; }
        public Vector2 CollectiblePos { get; private set; }
        public List<Collectible> collectibles;
        public Collectible collectible;

        public TileManager()
        {
            Tiles = GenerateMap("testbana.txt");
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
            int width = mapData[0].Length;
            int height = mapData.Length;
            Tile[,] tiles = new Tile[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Vector2 tilePosition = new Vector2(x * ConstantValues.tileWidth, y * ConstantValues.tileHeight);
                    if (mapData[y][x] == '1')
                    {
                        StartPositionPlayer = new Vector2(tilePosition.X, tilePosition.Y - ConstantValues.tileHeight);
                    }
                    if (mapData[y][x] == '2')
                    {
                        StartPositionMonster = tilePosition;
                    }
                    if (mapData[y][x] == '3')
                    {
                        CollectiblePos = new Vector2(tilePosition.X+ConstantValues.tileWidth/4,tilePosition.Y+ConstantValues.tileHeight/4);
                        collectible = new Collectible(TextureManager.CollectibleTex, CollectiblePos);
                        collectibles.Add(collectible);
                    }
                    tiles[x, y] = new Tile(tilePosition, mapData[y][x]);
                }
            }
            return tiles;
        }
    }
}
