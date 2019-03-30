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
    public class TileManager
    {
        public Tile[,] Tiles { get; private set; }
        public readonly int tileSize = 64;

        private Rectangle srcRectw, srcRectm, srcRectf;
        private Rectangle srcRectl, srcRecti, srcRectj, srcRectH, srcRectp, srcRectq, srcRectb, srcRectd;
        private Rectangle srcRecta, srcRecte, srcRectc, srcRecto, srcRectx, srcRectz, srcRects, srcRectv;

        public TileManager()
        {
            ReadMap();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Tiles.GetLength(0); i++)
            {
                for (int j = 0; j < Tiles.GetLength(1); j++)
                {
                    Tiles[i, j].Draw(spriteBatch);
                }
            }
        }

        public Tile GetTileAtPosition(Vector2 vector)
        {
            return Tiles[(int)vector.X / tileSize, (int)vector.Y / tileSize];
        }

        private void ReadMap()
        {
            List<string> strings = new List<string>();
            StreamReader sr = new StreamReader("maze.txt");

            while (!sr.EndOfStream)
            {
                strings.Add(sr.ReadLine());
            }
            sr.Close();

            Tiles = new Tile[strings[0].Length, strings.Count];

            SourceRectPositions();

            for (int i = 0; i < Tiles.GetLength(0); i++)
            {
                for (int j = 0; j < Tiles.GetLength(1); j++)
                {
                    if (strings[j][i] == 'w')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSize * i, tileSize * j), srcRectw, true);
                    }

                    else if (strings[j][i] == 'm')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSize * i, tileSize * j), srcRectm, true);
                    }

                    else if (strings[j][i] == 'H')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSize * i, tileSize * j), srcRectH, true);
                    }

                    else if (strings[j][i] == 'i')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSize * i, tileSize * j), srcRecti, true);
                    }

                    else if (strings[j][i] == 'j')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSize * i, tileSize * j), srcRectj, true);
                    }

                    else if (strings[j][i] == 'l')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSize * i, tileSize * j), srcRectl, true);
                    }

                    else if (strings[j][i] == 'p')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSize * i, tileSize * j), srcRectp, true);
                    }

                    else if (strings[j][i] == 'q')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSize * i, tileSize * j), srcRectq, true);
                    }

                    else if (strings[j][i] == 'b')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSize * i, tileSize * j), srcRectb, true);
                    }

                    else if (strings[j][i] == 'd')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSize * i, tileSize * j), srcRectd, true);
                    }

                    else if (strings[j][i] == 'a')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSize * i, tileSize * j), srcRecta, true);
                    }

                    else if (strings[j][i] == 'e')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSize * i, tileSize * j), srcRecte, true);
                    }

                    else if (strings[j][i] == 'c')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSize * i, tileSize * j), srcRectc, true);
                    }

                    else if (strings[j][i] == 'o')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSize * i, tileSize * j), srcRecto, true);
                    }

                    else if (strings[j][i] == 'x')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSize * i, tileSize * j), srcRectx, true);
                    }

                    else if (strings[j][i] == 'z')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSize * i, tileSize * j), srcRectz, true);
                    }

                    else if (strings[j][i] == 's')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSize * i, tileSize * j), srcRects, true);
                    }

                    else if (strings[j][i] == 'v')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSize * i, tileSize * j), srcRectv, true);
                    }

                    else if (strings[j][i] == '-')
                    {
                        Tiles[i, j] = new Tile(TextureManager.FloorTileTex, new Vector2(tileSize * i, tileSize * j), srcRectf, false);
                    }
                }
            }
        }

        private void SourceRectPositions()
        {
            srcRectw = new Rectangle(2 * tileSize, 1 * tileSize, tileSize, tileSize);
            srcRectm = new Rectangle(1 * tileSize, 1 * tileSize, tileSize, tileSize);

            srcRectl = new Rectangle(1 * tileSize, 0 * tileSize, tileSize, tileSize);
            srcRecti = new Rectangle(3 * tileSize, 0 * tileSize, tileSize, tileSize);
            srcRectj = new Rectangle(2 * tileSize, 0 * tileSize, tileSize, tileSize);
            srcRectH = new Rectangle(0 * tileSize, 0 * tileSize, tileSize, tileSize);

            srcRectp = new Rectangle(1 * tileSize, 2 * tileSize, tileSize, tileSize);
            srcRectq = new Rectangle(2 * tileSize, 2 * tileSize, tileSize, tileSize);
            srcRectb = new Rectangle(0 * tileSize, 2 * tileSize, tileSize, tileSize);
            srcRectd = new Rectangle(3 * tileSize, 2 * tileSize, tileSize, tileSize);

            srcRecta = new Rectangle(0 * tileSize, 3 * tileSize, tileSize, tileSize);
            srcRecte = new Rectangle(2 * tileSize, 3 * tileSize, tileSize, tileSize);
            srcRectc = new Rectangle(1 * tileSize, 3 * tileSize, tileSize, tileSize);
            srcRecto = new Rectangle(3 * tileSize, 3 * tileSize, tileSize, tileSize);

            srcRectx = new Rectangle(1 * tileSize, 4 * tileSize, tileSize, tileSize);
            srcRectz = new Rectangle(0 * tileSize, 4 * tileSize, tileSize, tileSize);
            srcRects = new Rectangle(2 * tileSize, 4 * tileSize, tileSize, tileSize);
            srcRectv = new Rectangle(3 * tileSize, 4 * tileSize, tileSize, tileSize);

            srcRectf = new Rectangle(0 * tileSize, 0 * tileSize, tileSize, tileSize);
        }
    }
}
