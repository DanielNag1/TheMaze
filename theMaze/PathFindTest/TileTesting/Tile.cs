using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileTesting
{
    public class Tile
    {
        private Texture2D texture;
        public Vector2 Position { get; private set; }
        private Rectangle sourceRect;
        private char identifier;
        private int frameSize = ConstantValues.TILE_FRAME_SIZE;

        public bool IsWall { get; private set; }
        public Rectangle Hitbox { get; private set; }

        public bool IsNotWall { get; private set; }

        public Tile(Vector2 position, char identifier)
        {
            this.Position = position;
            this.identifier = identifier;
            sourceRect = Rectangle.Empty;
            IsWall = true;
            
            IsNotWall = false;

            DeterminTexture();
            Hitbox = new Rectangle((int)position.X, (int)position.Y, ConstantValues.TILE_WIDTH, ConstantValues.TILE_HEIGHT);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)Position.X, (int)Position.Y, ConstantValues.TILE_WIDTH, ConstantValues.TILE_HEIGHT), sourceRect, Color.White);
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
                        IsWall = false;

                        IsNotWall = true;
                        break;
                    }
            }
        }
    }
}
