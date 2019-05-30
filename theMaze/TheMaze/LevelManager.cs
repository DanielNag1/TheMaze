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

        public List<Vector2> collectiblePositions;
        public List<WallMonster> wallMonsterList;
        public List<ArmMonster> armMonsterList;
        public List<GlitchMonster> glitchMonsterList;
        public List<Imbaku> imbakuList;
        public List<Stalker> stalkerList;
        public List<Golem> golemList;

        public ArmMonster armMonster;
        public WallMonster wallMonster;
        public GlitchMonster glitchMonster;
        public Imbaku imbaku;
        public Stalker stalker;
        public Golem golem;

        public Vector2 SuicideHallwayStopPosition { get; private set; }
        public List<Collectible> collectibles;
        public Collectible collectible;
        private bool isWhite;

        public LevelManager()
        {
            collectiblePositions = new List<Vector2>();
        }

        public void ReadDeathMap()
        {
            Tiles = GenerateMap("deathbana.txt",true);
        }

        public void ReadLevel1()
        {
            Tiles = GenerateMap("level1.txt",false);
            Pathfind.FillGridFromMap(Tiles);
        }

        public void ReadLevel2()
        {
            Tiles = GenerateMap("level2testtest.txt", false);
            Pathfind.FillGridFromMap(Tiles);
        }

        public void ReadLevel3()
        {
            armMonsterList.Clear();
            glitchMonsterList.Clear();
            stalkerList.Clear();
            Tiles = GenerateMap("level3.txt", false);
            Pathfind.FillGridFromMap(Tiles);
        }

        public void ReadLevel4()
        {
            golemList.Clear();
            imbakuList.Clear();
            armMonsterList.Clear();
            Tiles = GenerateMap("level4testtest.txt", false);
            Pathfind.FillGridFromMap(Tiles);
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

        private Tile[,] GenerateMap(string map,bool iswhite)
        {
            string[] mapData = File.ReadAllLines(map);
            collectibles = new List<Collectible>();
            wallMonsterList = new List<WallMonster>();
            glitchMonsterList = new List<GlitchMonster>();
            armMonsterList = new List<ArmMonster>();
            imbakuList = new List<Imbaku>();
            stalkerList = new List<Stalker>();
            golemList = new List<Golem>();

            int width = mapData[0].Length;
            int height = mapData.Length;
            Tile[,] tiles = new Tile[width, height];
            this.isWhite = iswhite;

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
                        imbaku = new Imbaku(TextureManager.ImbakuTex, tilePosition, this);
                        imbakuList.Add(imbaku);
                    }

                    if (mapData[y][x] == '3')
                    {
                        collectible = new Collectible(TextureManager.CollectibleTex, tilePosition);
                        Vector2 collectiblePositionForLight = new Vector2(collectible.Position.X + collectible.Texture.Width / 2, collectible.Position.Y + collectible.Texture.Height / 2);
                        collectiblePositions.Add(collectiblePositionForLight);
                        collectibles.Add(collectible);
                    }

                    if (mapData[y][x] == '4')
                    {
                        wallMonster = new WallMonster(TextureManager.WallMonsterTex, tilePosition);
                        wallMonsterList.Add(wallMonster);
                    }

                    if (mapData[y][x] == '5')
                    {
                        golem = new Golem(TextureManager.GolemTex, tilePosition, this);
                        golemList.Add(golem);
                    }

                    if (mapData[y][x] == '6')
                    {
                        glitchMonster = new GlitchMonster(TextureManager.FloorTileTex, tilePosition, this);
                        glitchMonsterList.Add(glitchMonster);
                    }

                    if (mapData[y][x] == '7')
                    {
                        stalker = new Stalker(TextureManager.StalkerTex, tilePosition, this);
                        stalkerList.Add(stalker);
                    }

                    if (mapData[y][x] == '8')
                    {
                        armMonster = new ArmMonster(TextureManager.ArmMonsterTex, tilePosition, this);
                        armMonsterList.Add(armMonster);
                    }

                    if (mapData[y][x] == '9')
                    {
                        SuicideHallwayStopPosition = tilePosition;
                    }
                    
                    tiles[x, y] = new Tile(tilePosition, mapData[y][x],isWhite);
                }
            }
            return tiles;
        }
    }
}