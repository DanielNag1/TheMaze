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
        public readonly int tileSizeX = 128;
        public readonly int tileSizeY = 227;

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
            return Tiles[(int)vector.X / tileSizeX, (int)vector.Y / tileSizeY];
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
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSizeX * i, tileSizeY * j), srcRectw, true);
                    }

                    else if (strings[j][i] == 'm')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSizeX * i, tileSizeY * j), srcRectm, true);
                    }

                    else if (strings[j][i] == 'H')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSizeX * i, tileSizeY * j), srcRectH, true);
                    }

                    else if (strings[j][i] == 'i')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSizeX * i, tileSizeY * j), srcRecti, true);
                    }

                    else if (strings[j][i] == 'j')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSizeX * i, tileSizeY * j), srcRectj, true);
                    }

                    else if (strings[j][i] == 'l')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSizeX * i, tileSizeY * j), srcRectl, true);
                    }

                    else if (strings[j][i] == 'p')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSizeX * i, tileSizeY * j), srcRectp, true);
                    }

                    else if (strings[j][i] == 'q')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSizeX * i, tileSizeY * j), srcRectq, true);
                    }

                    else if (strings[j][i] == 'b')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSizeX * i, tileSizeY * j), srcRectb, true);
                    }

                    else if (strings[j][i] == 'd')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSizeX * i, tileSizeY * j), srcRectd, true);
                    }

                    else if (strings[j][i] == 'a')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSizeX * i, tileSizeY * j), srcRecta, true);
                    }

                    else if (strings[j][i] == 'e')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSizeX * i, tileSizeY * j), srcRecte, true);
                    }

                    else if (strings[j][i] == 'c')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSizeX * i, tileSizeY * j), srcRectc, true);
                    }

                    else if (strings[j][i] == 'o')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSizeX * i, tileSizeY * j), srcRecto, true);
                    }

                    else if (strings[j][i] == 'x')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSizeX * i, tileSizeY * j), srcRectx, true);
                    }

                    else if (strings[j][i] == 'z')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSizeX * i, tileSizeY * j), srcRectz, true);
                    }

                    else if (strings[j][i] == 's')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSizeX * i, tileSizeY * j), srcRects, true);
                    }

                    else if (strings[j][i] == 'v')
                    {
                        Tiles[i, j] = new Tile(TextureManager.WallSheetTex, new Vector2(tileSizeX * i, tileSizeY * j), srcRectv, true);
                    }

                    else if (strings[j][i] == '-')
                    {
                        Tiles[i, j] = new Tile(TextureManager.FloorTileTex, new Vector2(tileSizeX * i, tileSizeY * j), srcRectf, false);
                    }
                }
            }
        }

        private void SourceRectPositions()
        {
            srcRectw = new Rectangle(2 * tileSizeX, 1 * tileSizeY, tileSizeX, tileSizeY);
            srcRectm = new Rectangle(1 * tileSizeX, 1 * tileSizeY, tileSizeX, tileSizeY);

            srcRectl = new Rectangle(1 * tileSizeX, 0 * tileSizeY, tileSizeX, tileSizeY);
            srcRecti = new Rectangle(3 * tileSizeX, 0 * tileSizeY, tileSizeX, tileSizeY);
            srcRectj = new Rectangle(2 * tileSizeX, 0 * tileSizeY, tileSizeX, tileSizeY);
            srcRectH = new Rectangle(0 * tileSizeX, 0 * tileSizeY, tileSizeX, tileSizeY);

            srcRectp = new Rectangle(1 * tileSizeX, 2 * tileSizeY, tileSizeX, tileSizeY);
            srcRectq = new Rectangle(2 * tileSizeX, 2 * tileSizeY, tileSizeX, tileSizeY);
            srcRectb = new Rectangle(0 * tileSizeX, 2 * tileSizeY, tileSizeX, tileSizeY);
            srcRectd = new Rectangle(3 * tileSizeX, 2 * tileSizeY, tileSizeX, tileSizeY);

            srcRecta = new Rectangle(0 * tileSizeX, 3 * tileSizeY, tileSizeX, tileSizeY);
            srcRecte = new Rectangle(2 * tileSizeX, 3 * tileSizeY, tileSizeX, tileSizeY);
            srcRectc = new Rectangle(1 * tileSizeX, 3 * tileSizeY, tileSizeX, tileSizeY);
            srcRecto = new Rectangle(3 * tileSizeX, 3 * tileSizeY, tileSizeX, tileSizeY);

            srcRectx = new Rectangle(1 * tileSizeX, 4 * tileSizeY, tileSizeX, tileSizeY);
            srcRectz = new Rectangle(0 * tileSizeX, 4 * tileSizeY, tileSizeX, tileSizeY);
            srcRects = new Rectangle(2 * tileSizeX, 4 * tileSizeY, tileSizeX, tileSizeY);
            srcRectv = new Rectangle(3 * tileSizeX, 4 * tileSizeY, tileSizeX, tileSizeY);

            srcRectf = new Rectangle(0 * tileSizeX, 0 * tileSizeY, tileSizeX, tileSizeY);
        }
    }
}
