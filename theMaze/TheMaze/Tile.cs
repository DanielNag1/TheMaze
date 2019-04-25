using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Penumbra;

namespace TheMaze
{
    public class Tile
    {
        private Texture2D texture;
        private Vector2 position;

        private Rectangle sourceRect;
        private char identifier;
        private int frameSize = ConstantValues.tileFrameSize;

        public Rectangle Hitbox { get; private set; }

        protected bool isWall = true;
        public bool IsWall
        {
            get { return isWall; }
        }

        public Tile(Vector2 position, char identifier)
        {
            this.position = position;
            this.identifier = identifier;
            sourceRect = Rectangle.Empty;

            Hitbox = new Rectangle((int)position.X, (int)position.Y, ConstantValues.tileWidth, ConstantValues.tileHeight);

            DeterminTexture();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, ConstantValues.tileWidth, ConstantValues.tileHeight), sourceRect, Color.White);
        }

        private void DeterminTexture()
        {
            switch (identifier)
            {
                case 'q':
                    {
                        texture = TextureManager.TopWallSheetTex;
                        sourceRect = new Rectangle(2 * frameSize, 2 * frameSize, frameSize, frameSize);
                        break;
                    }
                case 'p':
                    {
                        texture = TextureManager.TopWallSheetTex;
                        sourceRect = new Rectangle(1 * frameSize, 2 * frameSize, frameSize, frameSize);
                        break;
                    }
                case 'b':
                    {
                        texture = TextureManager.TopWallSheetTex;
                        sourceRect = new Rectangle(0 * frameSize, 2 * frameSize, frameSize, frameSize);
                        break;
                    }
                case 'd':
                    {
                        texture = TextureManager.TopWallSheetTex;
                        sourceRect = new Rectangle(3 * frameSize, 2 * frameSize, frameSize, frameSize);
                        break;
                    }
                case 'a':
                    {
                        texture = TextureManager.TopWallSheetTex;
                        sourceRect = new Rectangle(0 * frameSize, 1 * frameSize, frameSize, frameSize);
                        break;
                    }
                case 'e':
                    {
                        texture = TextureManager.TopWallSheetTex;
                        sourceRect = new Rectangle(2 * frameSize, 1 * frameSize, frameSize, frameSize);
                        break;
                    }
                case 'c':
                    {
                        texture = TextureManager.TopWallSheetTex;
                        sourceRect = new Rectangle(1 * frameSize, 1 * frameSize, frameSize, frameSize);
                        break;
                    }
                case 'o':
                    {
                        texture = TextureManager.TopWallSheetTex;
                        sourceRect = new Rectangle(3 * frameSize, 1 * frameSize, frameSize, frameSize);
                        break;
                    }
                case 'x':
                    {
                        texture = TextureManager.TopWallSheetTex;
                        sourceRect = new Rectangle(1 * frameSize, 3 * frameSize, frameSize, frameSize);
                        break;
                    }
                case 'v':
                    {
                        texture = TextureManager.TopWallSheetTex;
                        sourceRect = new Rectangle(3 * frameSize, 3 * frameSize, frameSize, frameSize);
                        break;
                    }
                case 'z':
                    {
                        texture = TextureManager.TopWallSheetTex;
                        sourceRect = new Rectangle(0 * frameSize, 3 * frameSize, frameSize, frameSize);
                        break;
                    }
                case 's':
                    {
                        texture = TextureManager.TopWallSheetTex;
                        sourceRect = new Rectangle(2 * frameSize, 3 * frameSize, frameSize, frameSize);
                        break;
                    }
                case 'w':
                    {
                        texture = TextureManager.TopWallSheetTex;
                        sourceRect = new Rectangle(2 * frameSize, 0 * frameSize, frameSize, frameSize);
                        break;
                    }
                case 'm':
                    {
                        texture = TextureManager.TopWallSheetTex;
                        sourceRect = new Rectangle(1 * frameSize, 0 * frameSize, frameSize, frameSize);
                        break;
                    }
                case 'H':
                    {
                        texture = TextureManager.WallSheetTex;
                        sourceRect = new Rectangle(3 * frameSize, 0 * frameSize, frameSize, frameSize);
                        break;
                    }
                case 'B':
                    {
                        texture = TextureManager.WallSheetTex;
                        sourceRect = new Rectangle(3 * frameSize, 1 * frameSize, frameSize, frameSize);
                        break;
                    }
                case 'l':
                    {
                        texture = TextureManager.WallSheetTex;
                        sourceRect = new Rectangle(0 * frameSize, 0 * frameSize, frameSize, frameSize);
                        break;
                    }
                case 'L':
                    {
                        texture = TextureManager.WallSheetTex;
                        sourceRect = new Rectangle(0 * frameSize, 1 * frameSize, frameSize, frameSize);
                        break;
                    }
                case 'i':
                    {
                        texture = TextureManager.WallSheetTex;
                        sourceRect = new Rectangle(2 * frameSize, 0 * frameSize, frameSize, frameSize);
                        break;
                    }
                case 'I':
                    {
                        texture = TextureManager.WallSheetTex;
                        sourceRect = new Rectangle(2 * frameSize, 1 * frameSize, frameSize, frameSize);
                        break;
                    }
                case 'j':
                    {
                        texture = TextureManager.WallSheetTex;
                        sourceRect = new Rectangle(1 * frameSize, 0 * frameSize, frameSize, frameSize);
                        break;
                    }
                case 'J':
                    {
                        texture = TextureManager.WallSheetTex;
                        sourceRect = new Rectangle(1 * frameSize, 1 * frameSize, frameSize, frameSize);
                        break;
                    }
                default:
                    {
                        texture = TextureManager.FloorTileTex;
                        sourceRect = new Rectangle(0, 0, frameSize, frameSize);
                        isWall = false;
                        break;
                    }
            }
        }

        public static readonly Vector2[] tilePoints =
        {
            new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0)
        };

        public static Hull HullFromRectangle(Rectangle bounds, int width)
        {
            return new Hull(tilePoints)
            {
                Position = new Vector2(bounds.X, bounds.Y),
                Scale = new Vector2(bounds.Width * width, bounds.Height)
            };
        }

    }
}
