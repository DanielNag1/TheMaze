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
        public char identifier;
        private int frameSize = ConstantValues.tileFrameSize;

        public Rectangle Hitbox { get; private set; }
        public Rectangle HullHitbox { get; private set; }

        protected bool isWall = true;
        protected bool isHull = false;
        public bool isWhite;
        public bool IsWall
        {
            get { return isWall; }
        }
        public bool IsHull
        {
            get { return isHull; }
        }

        public bool IsEntrance { get; private set; }


        public Tile(Vector2 position, char identifier, bool isWhite)
        {
            this.isWhite = isWhite;
            this.position = position;
            this.identifier = identifier;
            sourceRect = Rectangle.Empty;
            Hitbox = new Rectangle((int)position.X, (int)position.Y, ConstantValues.tileWidth, ConstantValues.tileHeight);
            IsEntrance = false;

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
                        HullHitbox = new Rectangle(Hitbox.X, Hitbox.Y + 40, ConstantValues.tileWidth - 30, ConstantValues.tileHeight / 3 + 20);
                        isHull = true;
                        break;
                    }
                case 'p':
                    {
                        texture = TextureManager.TopWallSheetTex;
                        sourceRect = new Rectangle(1 * frameSize, 2 * frameSize, frameSize, frameSize);
                        HullHitbox = new Rectangle(Hitbox.X + 25, Hitbox.Y + 40, ConstantValues.tileWidth + 50, ConstantValues.tileHeight / 3 + 20);
                        isHull = true;
                        break;
                    }
                case 'b':
                    {
                        texture = TextureManager.TopWallSheetTex;
                        sourceRect = new Rectangle(0 * frameSize, 2 * frameSize, frameSize, frameSize);
                        HullHitbox = new Rectangle(Hitbox.X + 35, Hitbox.Y + 30, ConstantValues.tileWidth / 2 + 60, ConstantValues.tileHeight / 3);
                        isHull = true;
                        break;
                    }
                case 'd':
                    {
                        texture = TextureManager.TopWallSheetTex;
                        sourceRect = new Rectangle(3 * frameSize, 2 * frameSize, frameSize, frameSize);
                        HullHitbox = new Rectangle(Hitbox.X + 40, Hitbox.Y - 35, ConstantValues.tileWidth / 3 + 22, ConstantValues.tileHeight / 3 + 20);
                        isHull = true;
                        break;
                    }
                case 'a':
                    {
                        texture = TextureManager.TopWallSheetTex;
                        sourceRect = new Rectangle(0 * frameSize, 1 * frameSize, frameSize, frameSize);
                        HullHitbox = new Rectangle(Hitbox.X + 35, Hitbox.Y - 20, ConstantValues.tileWidth / 3 + 10, ConstantValues.tileHeight / 2 + 30);
                        isHull = true;
                        break;
                    }
                case 'e':
                    {
                        texture = TextureManager.TopWallSheetTex;
                        sourceRect = new Rectangle(2 * frameSize, 1 * frameSize, frameSize, frameSize);
                        HullHitbox = new Rectangle(Hitbox.X + 50, Hitbox.Y + 26, ConstantValues.tileWidth / 3, ConstantValues.tileHeight + 40);
                        isHull = true;
                        break;
                    }
                case 'c':
                    {
                        texture = TextureManager.TopWallSheetTex;
                        sourceRect = new Rectangle(1 * frameSize, 1 * frameSize, frameSize, frameSize);
                        HullHitbox = new Rectangle(Hitbox.X + 40, Hitbox.Y + 26, ConstantValues.tileWidth + 50, ConstantValues.tileHeight / 2);
                        isHull = true;
                        break;
                    }
                case 'o':
                    {
                        texture = TextureManager.TopWallSheetTex;
                        sourceRect = new Rectangle(3 * frameSize, 1 * frameSize, frameSize, frameSize);
                        HullHitbox = new Rectangle(Hitbox.X + 25, Hitbox.Y + 50, ConstantValues.tileWidth / 3 + 20, ConstantValues.tileHeight / 3);
                        isHull = true;
                        break;
                    }
                case 'x':
                    {
                        texture = TextureManager.TopWallSheetTex;
                        sourceRect = new Rectangle(1 * frameSize, 3 * frameSize, frameSize, frameSize);
                        HullHitbox = new Rectangle(Hitbox.X + 40, Hitbox.Y + 50, ConstantValues.tileWidth + 15, ConstantValues.tileHeight / 2 - 5);
                        isHull = true;
                        break;
                    }
                case 'v':
                    {
                        texture = TextureManager.TopWallSheetTex;
                        sourceRect = new Rectangle(3 * frameSize, 3 * frameSize, frameSize, frameSize);
                        HullHitbox = new Rectangle(Hitbox.X + 50, Hitbox.Y - 25, ConstantValues.tileWidth / 3, ConstantValues.tileHeight + 75);
                        isHull = true;
                        break;
                    }
                case 'z':
                    {
                        texture = TextureManager.TopWallSheetTex;
                        sourceRect = new Rectangle(0 * frameSize, 3 * frameSize, frameSize, frameSize);
                        HullHitbox = new Rectangle(Hitbox.X + 50, Hitbox.Y + 50, ConstantValues.tileWidth, ConstantValues.tileHeight / 3);
                        isHull = true;
                        break;
                    }
                case 's':
                    {
                        texture = TextureManager.TopWallSheetTex;
                        sourceRect = new Rectangle(2 * frameSize, 3 * frameSize, frameSize, frameSize);
                        HullHitbox = new Rectangle(Hitbox.X - 20, Hitbox.Y + 25, ConstantValues.tileWidth + 70, ConstantValues.tileHeight / 3);
                        isHull = true;
                        break;
                    }
                case 'w':
                    {
                        if (isWhite)
                        {
                            texture = TextureManager.WhiteTopWallSheetTex;
                        }
                        else
                        {
                            texture = TextureManager.TopWallSheetTex;

                        }
                        sourceRect = new Rectangle(2 * frameSize, 0 * frameSize, frameSize, frameSize);
                        HullHitbox = new Rectangle(Hitbox.X - 20, Hitbox.Y + 25, ConstantValues.tileWidth + 70, ConstantValues.tileHeight / 3);
                        isHull = true;
                        break;
                    }
                case 'm':
                    {
                        texture = TextureManager.TopWallSheetTex;
                        sourceRect = new Rectangle(1 * frameSize, 0 * frameSize, frameSize, frameSize);
                        HullHitbox = new Rectangle(Hitbox.X + 50, Hitbox.Y - 25, ConstantValues.tileWidth / 3, ConstantValues.tileHeight + 75);
                        isHull = true;
                        break;
                    }
                case 'H':
                    {
                        if (isWhite)
                        {
                            texture = TextureManager.WhiteWallTex;
                        }
                        else
                        {
                            texture = TextureManager.WallSheetTex;
                        }
                        sourceRect = new Rectangle(0 * frameSize, 0 * frameSize, frameSize, frameSize);
                        break;
                    }
                case 'B':
                    {
                        if (isWhite)
                        {
                            texture = TextureManager.WhiteWallTex;
                        }
                        else
                        {
                            texture = TextureManager.WallSheetTex;
                        } 
                        sourceRect = new Rectangle(0 * frameSize, 1 * frameSize, frameSize, frameSize);
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
                        sourceRect = new Rectangle(0 * frameSize, 0 * frameSize, frameSize, frameSize);
                        break;
                    }
                case 'I':
                    {
                        texture = TextureManager.WallSheetTex;
                        sourceRect = new Rectangle(0 * frameSize, 1 * frameSize, frameSize, frameSize);
                        break;
                    }
                case 'j':
                    {
                        texture = TextureManager.WallSheetTex;
                        sourceRect = new Rectangle(0 * frameSize, 0 * frameSize, frameSize, frameSize);
                        break;
                    }
                case 'J':
                    {
                        texture = TextureManager.WallSheetTex;
                        sourceRect = new Rectangle(0 * frameSize, 1 * frameSize, frameSize, frameSize);
                        break;
                    }
                case '_':
                    {
                        texture = TextureManager.RedTexture;
                        sourceRect = new Rectangle(0 * frameSize, 0 * frameSize, frameSize, frameSize);
                        HullHitbox = new Rectangle(Hitbox.X, Hitbox.Y, ConstantValues.tileWidth, ConstantValues.tileHeight);
                        isHull = true;
                        break;
                    }
                case '4':
                    {
                        texture = TextureManager.WallSheetTex;
                        sourceRect = new Rectangle(0 * frameSize, 0 * frameSize, frameSize, frameSize);
                        break;
                    }
                case 'Y':
                    {
                        texture = TextureManager.FloorTileTex;
                        sourceRect = new Rectangle(0, 0, frameSize, frameSize);
                        isWall = false;
                        IsEntrance = true;
                        break;
                    }
                default:
                    {
                        if (isWhite)
                        {
                            texture = TextureManager.WaterTileTex;
                        }
                        else

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